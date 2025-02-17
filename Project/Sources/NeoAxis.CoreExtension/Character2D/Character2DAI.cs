﻿// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace NeoAxis
{
	/// <summary>
	/// Task-based artificial intelligence for 2D character.
	/// </summary>
	[AddToResourcesWindow( @"Base\2D\Character 2D AI", -7897 )]
	public class Character2DAI : AI
	{
		[Browsable( false )]
		public Character2D Character
		{
			get { return Parent as Character2D; }
		}

		protected override void OnSimulationStep()
		{
			base.OnSimulationStep();

			//task management
			var character = Character;
			if( character != null )
			{
				var task = CurrentTask;
				if( task != null )
				{
					//MoveToPosition, MoveToObject
					var moveTo = task as Character2DAITask_MoveTo;
					if( moveTo != null )
					{
						var moveToObject = moveTo as Character2DAITask_MoveToObject;
						if( moveToObject != null && ( moveToObject.Target.Value == null || !moveToObject.Target.Value.EnabledInHierarchy ) )
						{
							//no target
							if( task.DeleteTaskWhenReach )
								task.Dispose();
						}
						else
						{
							Vector2 target = Vector2.Zero;
							if( moveToObject != null )
								target = moveToObject.Target.Value.TransformV.Position.ToVector2();
							else if( moveTo is Character2DAITask_MoveToPosition moveToPosition )
								target = moveToPosition.Target;

							var diff = target - character.TransformV.Position.ToVector2();
							var distanceX = Math.Abs( diff.X );
							var distanceZ = Math.Abs( diff.Y );

							if( distanceX <= moveTo.DistanceToReach && distanceZ < character.Height )
							{
								//reach
								if( task.DeleteTaskWhenReach )
									task.Dispose();
							}
							else
							{
								//move character
								if( diff.X != 0 || diff.Y != 0 )
								{
									character.SetLookToDirection( diff );
									character.SetMoveVector( diff.X > 0 ? 1 : -1, moveTo.Run );
								}
							}
						}
					}
				}
			}
		}

		public void Stop()
		{
			ClearTaskQueue();
		}

		public Character2DAITask_MoveToPosition MoveTo( Vector2 target, bool run, bool clearTaskQueue = true )
		{
			if( clearTaskQueue )
				ClearTaskQueue();

			var task = CreateComponent<Character2DAITask_MoveToPosition>( enabled: false );
			task.Target = target;
			task.Run = run;
			task.Enabled = true;

			return task;
		}

		public Character2DAITask_MoveToObject MoveTo( ObjectInSpace target, bool run, bool clearTaskQueue = true )
		{
			if( clearTaskQueue )
				ClearTaskQueue();

			var task = CreateComponent<Character2DAITask_MoveToObject>( enabled: false );
			task.Target = target;
			task.Run = run;
			task.Enabled = true;

			return task;
		}
	}
}
