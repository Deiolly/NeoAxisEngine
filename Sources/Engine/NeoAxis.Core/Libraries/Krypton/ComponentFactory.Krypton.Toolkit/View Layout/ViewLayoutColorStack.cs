﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace Internal.ComponentFactory.Krypton.Toolkit
{
	internal class ViewLayoutColorStack : ViewLayoutStack
	{
		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewLayoutStack class.
		/// </summary>
        public ViewLayoutColorStack()
            : base(false)
        {
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutColorStack:" + Id;
		}
		#endregion

        #region Paint
        /// <summary>
        /// Perform a render of the elements.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void Render(RenderContext context)
        {
            Debug.Assert(context != null);

            // Perform rendering before any children
            RenderBefore(context);

            // Perform rendering after that of children
            RenderAfter(context);
        }

        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            // Ask each child to render in turn
            foreach (ViewBase child in this)
            {
                // Only render visible children that are inside the clipping rectangle
                if (child.Visible && child.ClientRectangle.IntersectsWith(context.ClipRect))
                    child.RenderBefore(context);
            }
        }

        /// <summary>
        /// Perform rendering after child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderAfter(RenderContext context)
        {
            // Ask each child to render in turn
            foreach (ViewBase child in this)
            {
                // Only render visible children that are inside the clipping rectangle
                if (child.Visible && child.ClientRectangle.IntersectsWith(context.ClipRect))
                    child.RenderAfter(context);
            }
        }
        #endregion

    }
}
