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
using System.ComponentModel;
using System.Drawing;

namespace Internal.ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Details for an cancellable event that provides a Point value.
	/// </summary>
    public class PointEventCancelArgs : CancelEventArgs
	{
		#region Instance Fields
        private Point _point;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PointEventCancelArgs class.
		/// </summary>
        /// <param name="point">Point associated with event.</param>
        public PointEventCancelArgs(Point point)
		{
            _point = point;
		}
		#endregion

        #region Point
        /// <summary>
		/// Gets and sets the point.
		/// </summary>
        public Point Point
		{
            get { return _point; }
            set { _point = value; }
		}
		#endregion
    }
}
