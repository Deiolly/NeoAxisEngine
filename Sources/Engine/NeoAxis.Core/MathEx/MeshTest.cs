﻿// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAxis
{
	/// <summary>
	/// Class for quickly determining the intersection of the ray with the mesh. Internally octree is used.
	/// </summary>
	public class MeshTest : IDisposable
	{
		OctreeContainer octreeContainer;
		Vector3F[] vertices;
		int[] indices;

		/////////////////////////////////////////

		public enum Mode
		{
			One,
			OneClosest,
			All,
		}

		/////////////////////////////////////////

		/// <summary>
		/// Represents result data for <see cref="MeshTest"/>.
		/// </summary>
		public struct ResultItem
		{
			internal float scale;
			public float Scale
			{
				get { return scale; }
				set { scale = value; }
			}

			internal int triangleIndex;
			public int TriangleIndex
			{
				get { return triangleIndex; }
				set { triangleIndex = value; }
			}

			public ResultItem( float scale, int triangleIndex )
			{
				this.scale = scale;
				this.triangleIndex = triangleIndex;
			}
		}

		/////////////////////////////////////////

		public MeshTest( Vector3F[] vertices, int[] indices )
		{
			this.vertices = vertices;
			this.indices = indices;

			if( vertices.Length != 0 && indices.Length != 0 )
			{
				var bounds = Bounds.Cleared;
				foreach( var vertex in vertices )
				{
					//!!!!new
					if( float.IsNaN( vertex.X ) || float.IsNaN( vertex.Y ) || float.IsNaN( vertex.Z ) )
						continue;

					bounds.Add( vertex );
				}

				var initSettings = new OctreeContainer.InitSettings();
				initSettings.InitialOctreeBounds = bounds;
				initSettings.OctreeBoundsRebuildExpand = Vector3.Zero;
				initSettings.MinNodeSize = bounds.GetSize() / 50;
				octreeContainer = new OctreeContainer( initSettings );

				for( int nTriangle = 0; nTriangle < indices.Length / 3; nTriangle++ )
				{
					ref var vertex0 = ref vertices[ indices[ nTriangle * 3 + 0 ] ];
					ref var vertex1 = ref vertices[ indices[ nTriangle * 3 + 1 ] ];
					ref var vertex2 = ref vertices[ indices[ nTriangle * 3 + 2 ] ];

					if( float.IsNaN( vertex0.X ) || float.IsNaN( vertex0.Y ) || float.IsNaN( vertex0.Z ) )
						continue;
					if( float.IsNaN( vertex1.X ) || float.IsNaN( vertex1.Y ) || float.IsNaN( vertex1.Z ) )
						continue;
					if( float.IsNaN( vertex2.X ) || float.IsNaN( vertex2.Y ) || float.IsNaN( vertex2.Z ) )
						continue;

					var triangleBounds = new Bounds( vertex0 );
					triangleBounds.Add( vertex1 );
					triangleBounds.Add( vertex2 );

					octreeContainer.AddObject( triangleBounds, 1 );
				}
			}
		}

		public void Dispose()
		{
			octreeContainer?.Dispose();
			octreeContainer = null;
		}

		public ResultItem[] RayCast( RayF ray, Mode mode, bool twoSided )
		{
			if( vertices.Length == 0 || indices.Length == 0 )
				return new ResultItem[ 0 ];
			if( ray.Direction == Vector3F.Zero )
				return new ResultItem[ 0 ];

			var octreeObjects = octreeContainer.GetObjects( ray, 0xFFFFFFFF );
			var resultList = new List<ResultItem>( octreeObjects.Length );
			foreach( var data in octreeObjects )
			{
				int triangleIndex = data.ObjectIndex;
				ref var vertex0 = ref vertices[ indices[ triangleIndex * 3 + 0 ] ];
				ref var vertex1 = ref vertices[ indices[ triangleIndex * 3 + 1 ] ];
				ref var vertex2 = ref vertices[ indices[ triangleIndex * 3 + 2 ] ];

				if( MathAlgorithms.IntersectTriangleRay( ref vertex0, ref vertex1, ref vertex2, ref ray, out float scale ) ||
					twoSided && MathAlgorithms.IntersectTriangleRay( ref vertex0, ref vertex2, ref vertex1, ref ray, out scale ) )
				{
					var item = new ResultItem( scale, triangleIndex );

					if( mode == Mode.One )
					{
						//mode One
						return new ResultItem[] { item };
					}
					else
					{
						//modes OneClosest, All
						if( resultList.Count == 0 )
							resultList.Add( item );
						else
						{
							if( mode == Mode.All )
								resultList.Add( item );
							else
							{
								if( item.scale < resultList[ 0 ].scale )
									resultList[ 0 ] = item;
							}
						}
					}
				}
			}

			ResultItem[] array = resultList.ToArray();
			if( mode == Mode.All )
			{
				CollectionUtility.SelectionSort( array, delegate ( ResultItem r1, ResultItem r2 )
				{
					if( r1.scale < r2.scale )
						return -1;
					if( r1.scale > r2.scale )
						return 1;
					return 0;
				} );
			}

			return resultList.ToArray();
		}

		public bool IntersectsFast( Plane[] planes, ref Bounds bounds )
		{
			if( vertices.Length == 0 || indices.Length == 0 )
				return false;
			var octreeObjects = octreeContainer.GetObjects( planes, bounds, 0xFFFFFFFF, OctreeContainer.ModeEnum.One, IntPtr.Zero );
			//сами треугольники тоже можно проверять. тогда это не Fast method
			return octreeObjects.Length != 0;
		}

		public bool IntersectsFast( Plane[] planes, Bounds bounds )
		{
			return IntersectsFast( planes, ref bounds );
		}

		public bool IntersectsFast( ref Bounds bounds )
		{
			if( vertices.Length == 0 || indices.Length == 0 )
				return false;
			var octreeObjects = octreeContainer.GetObjects( bounds, 0xFFFFFFFF, OctreeContainer.ModeEnum.One );
			//сами треугольники тоже можно проверять. тогда это не Fast method
			return octreeObjects.Length != 0;
		}

		public bool IntersectsFast( Bounds bounds )
		{
			return IntersectsFast( ref bounds );
		}

		public int[] GetIntersectedTrianglesFast( Plane[] planes, ref Bounds bounds )
		{
			if( vertices.Length == 0 || indices.Length == 0 )
				return Array.Empty<int>();
			var octreeObjects = octreeContainer.GetObjects( planes, bounds, 0xFFFFFFFF, OctreeContainer.ModeEnum.All, IntPtr.Zero );
			//сами треугольники тоже можно проверять. тогда это не Fast method
			return octreeObjects;
		}

		public int[] GetIntersectedTrianglesFast( Plane[] planes, Bounds bounds )
		{
			return GetIntersectedTrianglesFast( planes, ref bounds );
		}

		public int[] GetIntersectedTrianglesFast( ref Bounds bounds )
		{
			if( vertices.Length == 0 || indices.Length == 0 )
				return Array.Empty<int>();
			var octreeObjects = octreeContainer.GetObjects( bounds, 0xFFFFFFFF, OctreeContainer.ModeEnum.All );
			//сами треугольники тоже можно проверять. тогда это не Fast method
			return octreeObjects;
		}

		public int[] GetIntersectedTrianglesFast( Bounds bounds )
		{
			return GetIntersectedTrianglesFast( ref bounds );
		}
	}
}
