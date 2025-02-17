// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
using System;
using System.Collections.Generic;
using NeoAxis;

namespace Project
{
	public class UIStyleSimple : UIStyleDefault
	{
		protected override void OnBeforeRenderUIWithChildren( Component component, CanvasRenderer renderer )
		{
			base.OnBeforeRenderUIWithChildren( component, renderer );
		}

		protected override void OnAfterRenderUIWithChildren( Component component, CanvasRenderer renderer )
		{
			base.OnAfterRenderUIWithChildren( component, renderer );
		}

		/////////////////////////////////////////

		protected override void OnRenderButton( UIButton control, CanvasRenderer renderer )
		{
			if( control.Parent as UIContextMenu != null )
			{
				//context menu button

				var styleColor = ColorValue.Zero;
				switch( control.State )
				{
				case UIButton.StateEnum.Normal: styleColor = new ColorValue( 0.1, 0.1, 0.7 ); break;
				case UIButton.StateEnum.Hover: styleColor = new ColorValue( 0.175, 0.175, 0.75 ); break;
				case UIButton.StateEnum.Pushed: styleColor = new ColorValue( 0.25, 0.25, 0.9 ); break;
				case UIButton.StateEnum.Highlighted: styleColor = new ColorValue( 0.6, 0.6, 0 ); break;
				case UIButton.StateEnum.Disabled: styleColor = new ColorValue( 0.1, 0.1, 0.7 ); break;//new ColorValue( 0.5, 0.5, 0.5 ); break;
				}

				control.GetScreenRectangle( out var rect );
				var color = styleColor.GetSaturate();
				if( color.Alpha > 0 )
				{
					//back
					renderer.AddQuad( rect, color );

					//!!!!image

					//text
					var position = new Vector2( rect.Left + control.GetScreenOffsetByValueX( new UIMeasureValueDouble( UIMeasure.Units, 10 ) ), rect.GetCenter().Y ) + new Vector2( 0, renderer.DefaultFontSize / 10 );
					var textColor = control.State == UIButton.StateEnum.Disabled ? new ColorValue( 0.7, 0.7, 0.7 ) : new ColorValue( 1, 1, 1 );
					renderer.AddText( control.Text, position, EHorizontalAlignment.Left, EVerticalAlignment.Center, textColor );
				}
			}
			else
			{
				//usual button

				var styleColor = ColorValue.Zero;
				switch( control.State )
				{
				case UIButton.StateEnum.Normal: styleColor = control.Focused ? new ColorValue( 0.2, 0.2, 0.8 ) : new ColorValue( 0.1, 0.1, 0.6 ); break;
				case UIButton.StateEnum.Hover: styleColor = new ColorValue( 0.175, 0.175, 0.75 ); break;
				case UIButton.StateEnum.Pushed: styleColor = new ColorValue( 0.25, 0.25, 0.9 ); break;
				case UIButton.StateEnum.Highlighted: styleColor = control.Focused ? new ColorValue( 0.8, 0.8, 0 ) : new ColorValue( 0.6, 0.6, 0 ); break;
				case UIButton.StateEnum.Disabled: styleColor = new ColorValue( 0.1, 0.1, 0.7 ); break;
					//case UIButton.StateEnum.Disabled: styleColor = new ColorValue( 0.8, 0.8, 0.8 ); break;
					//case UIButton.StateEnum.Disabled: styleColor = new ColorValue( 0.5, 0.5, 0.5 ); break;
				}

				control.GetScreenRectangle( out var rect );
				var color = styleColor.GetSaturate();
				if( color.Alpha > 0 )
				{
					//back
					renderer.AddQuad( rect, color );

					//image
					if( control.Image.Value != null )
					{
						var image = control.Image.Value;
						if( control.ReadOnly && control.ImageDisabled.Value != null )
							image = control.ImageDisabled.Value;

						var imageRect = rect;
						imageRect.Expand( -control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 4, 4 ) ) );
						renderer.AddQuad( imageRect, new Rectangle( 0, 0, 1, 1 ), image, new ColorValue( 1, 1, 1 ), true );
					}

					//text
					var position = rect.GetCenter() + new Vector2( 0, renderer.DefaultFontSize / 10 );
					var textColor = control.State == UIButton.StateEnum.Disabled ? new ColorValue( 0.5, 0.5, 1 ) : new ColorValue( 1, 1, 1 );
					//var textColor = control.State == UIButton.StateEnum.Disabled ? new ColorValue( 0.7, 0.7, 0.7 ) : new ColorValue( 1, 1, 1 );
					renderer.AddText( control.Text, position, EHorizontalAlignment.Center, EVerticalAlignment.Center, textColor );
				}
			}
		}

		/////////////////////////////////////////

		protected override void OnRenderCheck( UICheck control, CanvasRenderer renderer )
		{
			var borderColor = control.Focused ? new ColorValue( 0.7, 0.7, 0.7 ) : new ColorValue( 0.5, 0.5, 0.5 );
			var insideColor = new ColorValue( 0, 0, 0 );

			//var checkColor = new ColorValue( 0.8, 0.8, 0.8 );
			var checkColor = new ColorValue( 1, 1, 0 );
			var textColor = new ColorValue( 1, 1, 1 );

			switch( control.State )
			{
			//case UICheck.StateEnum.Hover:
			//	checkColor = new ColorValue( 1, 1, 1 );
			//	break;

			case UICheck.StateEnum.Pushed:
				checkColor = new ColorValue( 1, 1, 1 );
				break;

			case UICheck.StateEnum.Disabled:
				borderColor = new ColorValue( 0.8, 0.8, 0.8 );
				checkColor = new ColorValue( 0.8, 0.8, 0.8 );
				textColor = new ColorValue( 1, 1, 1 );
				//checkColor = new ColorValue( 0.7, 0.7, 0.7 );
				//textColor = new ColorValue( 0.5, 0.5, 0.5 );
				break;
			}

			var colorMultiplier = new ColorValue( 1, 1, 1 );
			//var colorMultiplier = control.GetTotalColorMultiplier();
			//if( colorMultiplier.Alpha > 0 )
			//{
			control.GetScreenRectangle( out var controlRect );

			var imageRect = new Rectangle( controlRect.Left, controlRect.Top, controlRect.Left + controlRect.Size.Y * renderer.AspectRatioInv, controlRect.Bottom );

			renderer.AddQuad( imageRect, borderColor * colorMultiplier );
			renderer.AddQuad( Multiply( imageRect, new Rectangle( 0.1, 0.1, 0.9, 0.9 ) ), insideColor * colorMultiplier );

			//Checked image
			if( control.Checked.Value == UICheck.CheckValue.Checked )
			{
				var points = new Vector2[]
				{
					new Vector2( 290.04, 33.286 ),
					new Vector2( 118.861, 204.427 ),
					new Vector2( 52.32, 137.907 ),
					new Vector2( 0, 190.226 ),
					new Vector2( 118.861, 309.071 ),
					new Vector2( 342.357, 85.606 ),
				};
				var points2 = new Vector2[ points.Length ];
				for( int n = 0; n < points2.Length; n++ )
					points2[ n ] = points[ n ] / new Vector2( 342.357, 342.357 );

				var color2 = checkColor * colorMultiplier;

				var vertices = new CanvasRenderer.TriangleVertex[ points2.Length ];
				for( int n = 0; n < points2.Length; n++ )
					vertices[ n ] = new CanvasRenderer.TriangleVertex( Multiply( imageRect, points2[ n ] ).ToVector2F(), color2 );

				var indices = new int[] { 0, 1, 5, 5, 4, 1, 1, 2, 3, 3, 1, 4 };

				renderer.AddTriangles( vertices, indices );
			}

			//Indeterminate image
			if( control.Checked.Value == UICheck.CheckValue.Indeterminate )
				renderer.AddQuad( Multiply( imageRect, new Rectangle( 0.3, 0.3, 0.7, 0.7 ) ), checkColor * colorMultiplier );

			//!!!!������� ������ ���� ����, ��� ����������
			//text
			renderer.AddText( " " + control.Text, new Vector2( imageRect.Right, imageRect.GetCenter().Y ), EHorizontalAlignment.Left, EVerticalAlignment.Center, textColor * colorMultiplier );
			//renderer.AddText( " " + control.Text, new Vector2( rect.Right, rect.Top ), EHorizontalAlignment.Left, EVerticalAlignment.Top, textColor * colorMultiplier );
			//}


			//base.OnRenderCheck( control, renderer );
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
			var rect = control.GetScreenRectangle();
			renderer.AddQuad( rect, new ColorValue( 0, 0, 0 ) );
			//renderer.AddQuad( rect, new ColorValue( 0.2, 0.2, 0.2 ) );

			var rect2 = rect;
			rect2.Expand( -control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 2, 2 ) ) );

			//border
			var color = control.Focused ? new ColorValue( 1, 1, 1 ) : new ColorValue( 0.75, 0.75, 0.75 );
			renderer.AddQuad( new Rectangle( rect.Left, rect.Top, rect2.Left, rect.Bottom ), color );
			renderer.AddQuad( new Rectangle( rect2.Left, rect.Top, rect2.Right, rect2.Top ), color );
			renderer.AddQuad( new Rectangle( rect2.Right, rect.Top, rect.Right, rect.Bottom ), color );
			renderer.AddQuad( new Rectangle( rect.Left, rect2.Bottom, rect2.Right, rect.Bottom ), color );

			var font = control.Font.Value;
			if( font == null )
				font = renderer.DefaultFont;
			var fontSize = control.GetFontSizeScreen();
			var itemSize = GetListItemSizeScreen( control, renderer );
			var totalItemsHeight = itemSize * control.Items.Count;
			var scrollBar = control.GetScroll();

			//!!!!���?
			//update scroll bar properties
			if( scrollBar != null )
			{
				double screenSizeY = rect2.Size.Y;
				double scrollScreenSizeY = totalItemsHeight - screenSizeY;

				scrollBar.Visible = control.AlwaysShowScroll || totalItemsHeight > screenSizeY;
				if( scrollBar.Visible )
					scrollBar.ValueRange = new NeoAxis.Range( 0, scrollScreenSizeY );

				//ensure visible
				if( control.NeedEnsureVisibleInStyle != -1 )
				{
					var index = control.NeedEnsureVisibleInStyle;

					if( (float)index * itemSize > screenSizeY / 2 )
					{
						var factor = (float)index / (float)( control.Items.Count - 1 );
						var v = scrollScreenSizeY * factor;
						scrollBar.Value = MathEx.Clamp( v, 0, scrollBar.ValueRange.Value.Maximum );
					}
					else
						scrollBar.Value = 0;

					control.NeedEnsureVisibleInStyle = -1;
				}

				//if( scrollBar.Visible )
				//{
				//	if( scrollScreenSizeY > 0 )
				//	{
				//		double currentScrollScreenPosY = scrollBar.Value * scrollScreenSizeY;

				//		double itemScrollScreenPosY = itemSize * (double)control.SelectedIndex;
				//		Range itemScrollScreenRangeY = new Range( itemScrollScreenPosY, itemScrollScreenPosY + itemSize );

				//		if( itemScrollScreenRangeY.Minimum < currentScrollScreenPosY )
				//		{
				//			currentScrollScreenPosY = itemScrollScreenRangeY.Minimum;
				//		}
				//		else
				//		{
				//			if( itemScrollScreenRangeY.Maximum > currentScrollScreenPosY + screenSizeY )
				//				currentScrollScreenPosY = itemScrollScreenRangeY.Maximum + itemSize - screenSizeY;
				//		}

				//		scrollBar.Value = currentScrollScreenPosY / scrollScreenSizeY;
				//	}
				//	else
				//		scrollBar.Value = 0;
				//}
				//else
				//	scrollBar.Value = 0;
			}

			//items
			if( control.Items.Count != 0 )
			{
				renderer.PushClipRectangle( rect2 );

				var positionY = rect2.Top;
				if( scrollBar != null && scrollBar.VisibleInHierarchy && scrollBar.EnabledInHierarchy )
					positionY -= scrollBar.Value;

				for( int n = 0; n < control.Items.Count; n++ )
				{
					var item = control.Items[ n ];
					var itemRectangle = new Rectangle( rect2.Left, positionY, rect2.Right, positionY + itemSize );
					if( scrollBar != null && scrollBar.EnabledInHierarchy && scrollBar.VisibleInHierarchy )
						itemRectangle.Right -= scrollBar.GetScreenSize().X;

					if( itemRectangle.Intersects( rect2 ) )
					{
						renderer.PushClipRectangle( itemRectangle );

						if( n == control.SelectedIndex )
						{
							var color2 = new ColorValue( 0.1, 0.1, 0.8 );
							//var color2 = new ColorValue( 0.5, 0.5, 0.5 );
							//var color2 = control.ReadOnlyInHierarchy ? new ColorValue( 0.5, 0.5, 0.5 ) : new ColorValue( 0, 0, 0.8 );
							renderer.AddQuad( itemRectangle, color2 );
						}

						var positionX = rect2.Left + control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 2, 0 ) ).X;
						renderer.AddText( font, fontSize, item, new Vector2( positionX, itemRectangle.GetCenter().Y ), EHorizontalAlignment.Left, EVerticalAlignment.Center, new ColorValue( 1, 1, 1 ) );

						renderer.PopClipRectangle();
					}

					positionY += itemSize;
				}

				renderer.PopClipRectangle();
			}


			//base.OnRenderList( control, renderer );
		}

		public override int GetListItemIndexByScreenPosition( UIList control, Vector2 position )
		{
			return base.GetListItemIndexByScreenPosition( control, position );
		}

		/////////////////////////////////////////

		protected override void OnRenderWindow( UIWindow control, CanvasRenderer renderer )
		{
			var rect = control.GetScreenRectangle();
			renderer.AddQuad( rect, new ColorValue( 0.05, 0.05, 0.25 ) );

			var rect2 = rect;
			rect2.Expand( -control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 4, 4 ) ) );

			var color = new ColorValue( 0.25, 0.25, 0.75 );
			renderer.AddQuad( new Rectangle( rect.Left, rect.Top, rect2.Left, rect.Bottom ), color );
			renderer.AddQuad( new Rectangle( rect2.Left, rect.Top, rect2.Right, rect2.Top ), color );
			renderer.AddQuad( new Rectangle( rect2.Right, rect.Top, rect.Right, rect.Bottom ), color );
			renderer.AddQuad( new Rectangle( rect.Left, rect2.Bottom, rect2.Right, rect.Bottom ), color );

			if( control.TitleBar.Value )
			{
				double titleBarHeight = 30;
				double screenY = rect.Top + control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 0, titleBarHeight ) ).Y;
				double screenY2 = screenY + control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 0, 4 ) ).Y;

				var rect3 = new Rectangle( rect2.Left, rect2.Top, rect2.Right, screenY2 );
				renderer.AddQuad( rect3, color );

				if( !string.IsNullOrEmpty( control.Text ) )
				{
					var pos = new Vector2( rect.GetCenter().X, ( rect2.Top + screenY ) / 2 );
					renderer.AddText( control.Text, pos, EHorizontalAlignment.Center, EVerticalAlignment.Center, new ColorValue( 1, 1, 1 ) );
				}
			}
		}

		/////////////////////////////////////////

		protected override void OnRenderProgress( UIProgress control, CanvasRenderer renderer )
		{
			var rect = control.GetScreenRectangle();
			renderer.AddQuad( rect, new ColorValue( 0.05, 0.05, 0.3 ) );

			if( control.Maximum.Value != 0 )
			{
				double progress = control.Value.Value / control.Maximum.Value;
				if( progress > 0 )
				{
					var rect2 = rect;
					rect2.Expand( -control.GetScreenOffsetByValue( new UIMeasureValueVector2( UIMeasure.Units, 4, 4 ) ) );
					rect2.Right = MathEx.Lerp( rect2.Left, rect2.Right, progress );

					renderer.AddQuad( rect2, new ColorValue( 1, 1, 1 ) );
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

		protected override void OnRenderContextMenu( UIContextMenu menu, CanvasRenderer renderer )
		{
			base.OnRenderContextMenu( menu, renderer );

			//draw background
			renderer.AddQuad( menu.GetScreenRectangle(), new ColorValue( 0.1, 0.1, 0.7 ) );
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