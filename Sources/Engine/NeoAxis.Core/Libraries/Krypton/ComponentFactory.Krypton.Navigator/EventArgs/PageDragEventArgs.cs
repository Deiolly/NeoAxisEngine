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

namespace Internal.ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Details for an event that provides pages associated with a page dragging event.
	/// </summary>
	public class PageDragEventArgs : EventArgs
	{
		#region Instance Fields
        private Point _screenPoint;
        private KryptonPageCollection _pages;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the KryptonPageDragEventArgs class.
		/// </summary>
        /// <param name="screenPoint">Screen point of the mouse.</param>
        /// <param name="pages">Array of event associated pages.</param>
        public PageDragEventArgs(Point screenPoint,
                                 KryptonPage[] pages)
		{
            _screenPoint = screenPoint;
            _pages = new KryptonPageCollection();

            if (pages != null)
                _pages.AddRange(pages);
		}

        /// <summary>
        /// Initialize a new instance of the KryptonPageDragEventArgs class.
        /// </summary>
        /// <param name="screenPoint">Screen point of the mouse.</param>
        /// <param name="pages">Collection of event associated pages.</param>
        public PageDragEventArgs(Point screenPoint,
                                 KryptonPageCollection pages)
        {
            _screenPoint = screenPoint;
            _pages = pages;
        }
        #endregion

        #region ScreenPoint
        /// <summary>
        /// Gets access to the associated screen point.
        /// </summary>
        public Point ScreenPoint
        {
            get { return _screenPoint; }
        }
        #endregion

        #region Pages
        /// <summary>
        /// Gets access to the collection of pages.
        /// </summary>
        public KryptonPageCollection Pages
        {
            get { return _pages; }
        }
        #endregion
    }
}
