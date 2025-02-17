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
    /// Details for an event that provides a new index position for a specified page.
	/// </summary>
	public class TabMovedEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonPage _page;
        private int _index;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the TabMovedEventArgs class.
		/// </summary>
        /// <param name="page">Reference to page that has been moved.</param>
        /// <param name="index">New index of the page within the page collection.</param>
        public TabMovedEventArgs(KryptonPage page, int index)
		{
            _page = page;
            _index = index;
		}
        #endregion

        #region Dropped
        /// <summary>
        /// Gets a reference to the page that has been moved.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
        }
        #endregion

        #region Pages
        /// <summary>
        /// Gets the new index of the page within the page collection.
        /// </summary>
        public int Index
        {
            get { return _index; }
        }
        #endregion
    }
}
