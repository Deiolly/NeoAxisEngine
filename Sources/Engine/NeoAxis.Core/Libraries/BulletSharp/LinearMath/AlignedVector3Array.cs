﻿using Internal.BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(Vector3ListDebugView))]
	public class AlignedVector3Array : IList<BVector3>, IDisposable
	{
		internal IntPtr _native;
		private bool _preventDelete;

		internal AlignedVector3Array(IntPtr native)
		{
			_native = native;
			_preventDelete = true;
		}

		public AlignedVector3Array()
		{
			_native = btAlignedObjectArray_btVector3_new();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					btAlignedObjectArray_btVector3_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~AlignedVector3Array()
		{
			Dispose(false);
		}

		public int IndexOf(BVector3 item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, BVector3 item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public BVector3 this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				BVector3 value;
				btAlignedObjectArray_btVector3_at(_native, index, out value);
				return value;
			}
			set
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				btAlignedObjectArray_btVector3_set(_native, index, ref value);
			}
		}

		public void Add(BVector3 item)
		{
			btAlignedObjectArray_btVector3_push_back(_native, ref item);
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(BVector3 item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(BVector3[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));

			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(array));

			int count = Count;
			if (arrayIndex + count > array.Length)
				throw new ArgumentException("Array too small.", "array");

			for (int i = 0; i < count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		public int Count => btAlignedObjectArray_btVector3_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(BVector3 item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<BVector3> GetEnumerator()
		{
			return new Vector3ArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new Vector3ArrayEnumerator(this);
		}
	}
}
