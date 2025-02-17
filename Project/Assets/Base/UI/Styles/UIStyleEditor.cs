// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
using System;
using System.Collections.Generic;
using NeoAxis;

namespace Project
{
	public class UIStyleEditor : UIStyleDefault
	{
		bool DarkTheme
		{
			get
			{
#if !DEPLOY
				if( EngineApp.ApplicationType == EngineApp.ApplicationTypeEnum.Editor )
					return NeoAxis.Editor.EditorAPI.DarkTheme;
#endif
				return true;
			}
		}

		protected override void OnRenderButton( UIButton control, CanvasRenderer renderer )
		{
			//!!!!context menu

			//if( control.Parent as UIContextMenu != null )
			//{
			//	//context menu button
			//}
			//else
			//{
			//	//usual button
			//}

			//!!!!
			//RenderButtonTextImage( control, renderer, new ColorValue( 1, 1, 1 ), new ColorValue( 1, 1, 1 ) );

			control.GetScreenRectangle( out var rect );

			//back
			{
				var color = new ColorByte();
				switch( control.State )
				{
				case UIButton.StateEnum.Normal: color = DarkTheme ? new ColorByte( 60, 60, 60 ) : new ColorByte( 253, 253, 253 ); break;
				case UIButton.StateEnum.Hover: color = DarkTheme ? new ColorByte( 80, 80, 80 ) : new ColorByte( 205, 230, 247 ); break;
				case UIButton.StateEnum.Pushed: color = DarkTheme ? new ColorByte( 90, 90, 90 ) : new ColorByte( 204, 228, 247 ); break;
				case UIButton.StateEnum.Highlighted: color = DarkTheme ? new ColorByte( 90, 90, 90 ) : new ColorByte( 204, 228, 247 ); break;
				case UIButton.StateEnum.Disabled: color = DarkTheme ? new ColorByte( 50, 50, 50 ) : new ColorByte( 250, 250, 250 ); break;
				}

				renderer.AddQuad( rect, color.ToColorValue() );
			}

			//text
			{
				var color = new ColorByte();
				switch( control.State )
				{
				case UIButton.StateEnum.Normal: color = DarkTheme ? new ColorByte( 230, 230, 230 ) : new ColorByte( 59, 59, 59 ); break;
				case UIButton.StateEnum.Hover: color = DarkTheme ? new ColorByte( 230, 230, 230 ) : new ColorByte( 0, 0, 0 ); break;
				case UIButton.StateEnum.Pushed: color = DarkTheme ? new ColorByte( 230, 230, 230 ) : new ColorByte( 0, 0, 0 ); break;
				case UIButton.StateEnum.Highlighted: color = DarkTheme ? new ColorByte( 230, 230, 230 ) : new ColorByte( 0, 0, 0 ); break;
				case UIButton.StateEnum.Disabled: color = DarkTheme ? new ColorByte( 90, 90, 90 ) : new ColorByte( 167, 167, 167 ); break;
				}

				var center = rect.GetCenter() + new Vector2( 0, renderer.DefaultFontSize / 10 );
				renderer.AddText( control.Text, center, EHorizontalAlignment.Center, EVerticalAlignment.Center, color.ToColorValue() );
			}

			//border
			{
				var color = new ColorByte();
				switch( control.State )
				{
				case UIButton.StateEnum.Normal: color = DarkTheme ? new ColorByte( 90, 90, 90 ) : new ColorByte( 170, 170, 170 ); break;
				case UIButton.StateEnum.Hover: color = DarkTheme ? new ColorByte( 130, 130, 130 ) : new ColorByte( 95, 168, 226 ); break;
				case UIButton.StateEnum.Pushed: color = DarkTheme ? new ColorByte( 150, 150, 150 ) : new ColorByte( 0, 120, 215 ); break;
				case UIButton.StateEnum.Highlighted: color = DarkTheme ? new ColorByte( 150, 150, 150 ) : new ColorByte( 0, 120, 215 ); break;
				case UIButton.StateEnum.Disabled: color = DarkTheme ? new ColorByte( 60, 60, 60 ) : new ColorByte( 180, 180, 180 ); break;
				}

				renderer.AddRectangle( rect, color.ToColorValue() );
			}
		}

		/////////////////////////////////////////

		protected override void OnRenderCheck( UICheck control, CanvasRenderer renderer )
		{
			base.OnRenderCheck( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderEdit( UIEdit control, CanvasRenderer renderer )
		{
			base.OnRenderEdit( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderText( UIText control, CanvasRenderer renderer )
		{
			base.OnRenderText( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderScroll( UIScroll control, CanvasRenderer renderer )
		{
			base.OnRenderScroll( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderList( UIList control, CanvasRenderer renderer )
		{
			base.OnRenderList( control, renderer );
		}

		public override int GetListItemIndexByScreenPosition( UIList control, Vector2 position )
		{
			return base.GetListItemIndexByScreenPosition( control, position );
		}

		/////////////////////////////////////////

		protected override void OnRenderWindow( UIWindow control, CanvasRenderer renderer )
		{
			base.OnRenderWindow( control, renderer );

			//var rect = control.GetScreenRectangle();
			//renderer.AddQuad( rect, new ColorValue( 0.05, 0.05, 0.25 ) );

			//var rect2 = rect;
			//rect2.Expand( -control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 4, 4 ) ) );

			//var color = new ColorValue( 0.25, 0.25, 0.75 );
			//renderer.AddQuad( new Rectangle( rect.Left, rect.Top, rect2.Left, rect.Bottom ), color );
			//renderer.AddQuad( new Rectangle( rect2.Left, rect.Top, rect2.Right, rect2.Top ), color );
			//renderer.AddQuad( new Rectangle( rect2.Right, rect.Top, rect.Right, rect.Bottom ), color );
			//renderer.AddQuad( new Rectangle( rect.Left, rect2.Bottom, rect2.Right, rect.Bottom ), color );

			//if( control.TitleBar.Value )
			//{
			//	double titleBarHeight = 30;
			//	double screenY = rect.Top + control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 0, titleBarHeight ) ).Y;
			//	double screenY2 = screenY + control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 0, 4 ) ).Y;

			//	var rect3 = new Rectangle( rect2.Left, rect2.Top, rect2.Right, screenY2 );
			//	renderer.AddQuad( rect3, color );

			//	if( !string.IsNullOrEmpty( control.Text ) )
			//	{
			//		var pos = new Vector2( rect.GetCenter().X, ( rect2.Top + screenY ) / 2 );
			//		renderer.AddText( control.Text, pos, EHorizontalAlignment.Center, EVerticalAlignment.Center, new ColorValue( 1, 1, 1 ) );
			//	}
			//}
		}

		/////////////////////////////////////////

		protected override void OnRenderProgress( UIProgress control, CanvasRenderer renderer )
		{
			control.GetScreenRectangle( out var rect );

			//back
			{
				var color = DarkTheme ? new ColorByte( 40, 40, 40 ) : new ColorByte( 230, 230, 230 );
				renderer.AddQuad( rect, color.ToColorValue() );
			}

			//border
			{
				var color = DarkTheme ? new ColorByte( 80, 80, 80 ) : new ColorByte( 188, 188, 188 );
				renderer.AddRectangle( rect, color.ToColorValue() );
			}

			//progress
			if( control.Maximum.Value != 0 )
			{
				double progress = control.Value.Value / control.Maximum.Value;
				if( progress > 0 )
				{
					var color = DarkTheme ? new ColorByte( 0, 150, 0 ) : new ColorByte( 0, 190, 0 );

					var rect2 = rect;
					//!!!!Pixels?
					rect2.Expand( -control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 2, 2 ) ) );
					rect2.Right = MathEx.Lerp( rect2.Left, rect2.Right, progress );

					renderer.AddQuad( rect2, color.ToColorValue() );
				}
			}
		}

		/////////////////////////////////////////

		protected override void OnRenderSlider( UISlider control, CanvasRenderer renderer )
		{
			base.OnRenderSlider( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderGrid( UIGrid control, CanvasRenderer renderer )
		{
			base.OnRenderGrid( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderCombo( UICombo control, CanvasRenderer renderer )
		{
			base.OnRenderCombo( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderTooltip( UITooltip tooltip, CanvasRenderer renderer )
		{
			base.OnRenderTooltip( tooltip, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderContextMenu( UIContextMenu control, CanvasRenderer renderer )
		{
			base.OnRenderContextMenu( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderToolbar( UIToolbar control, CanvasRenderer renderer )
		{
			base.OnRenderToolbar( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderSplitContainer( UISplitContainer control, CanvasRenderer renderer )
		{
			base.OnRenderSplitContainer( control, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderTabControl( UITabControl control, CanvasRenderer renderer )
		{
			base.OnRenderTabControl( control, renderer );
		}

	}
}