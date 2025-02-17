using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Internal.Aga.Controls.Tree.NodeControls
{
	public class DrawTextEventArgs : DrawEventArgs
	{
		private Brush _backgroundBrush;
		public Brush BackgroundBrush
		{
            get { return _backgroundBrush; }
			set { _backgroundBrush = value; }
		}

		private Font _font;
		public Font Font
		{
			get { return _font; }
			set { _font = value; }
		}

		private Color _textColor;
		public Color TextColor
		{
			get { return _textColor; }
			set { _textColor = value; }
		}

		private string _text;
		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public DrawTextEventArgs(TreeNodeAdv node, NodeControl control, DrawContext context, string text)
			: base(node, control, context)
		{
			_text = text;
		}
	}
}
