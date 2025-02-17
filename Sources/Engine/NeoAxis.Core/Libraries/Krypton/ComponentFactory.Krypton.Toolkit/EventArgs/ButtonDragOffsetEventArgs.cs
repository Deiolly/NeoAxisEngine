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
using System.Drawing;
using System.Windows.Forms;

namespace Internal.ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Details for an event that provides a button drag offset value.
	/// </summary>
	public class ButtonDragOffsetEventArgs : EventArgs
	{
		#region Instance Fields
        private Point _offset;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ButtonDragOffsetEventArgs class.
		/// </summary>
        /// <param name="offset">Mouse offset for button dragging.</param>
        public ButtonDragOffsetEventArgs(Point offset)
		{
            _offset = offset;
		}
        #endregion

        #region Point
        /// <summary>
        /// Gets access to the left mouse dragging offer.
        /// </summary>
        public Point PointOffset
        {
            get { return _offset; }
        }
        #endregion
    }
}
