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
using System.Diagnostics;
using Internal.ComponentFactory.Krypton.Toolkit;
using Internal.ComponentFactory.Krypton.Navigator;
using Internal.ComponentFactory.Krypton.Workspace;

namespace Internal.ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for a AutoHiddenGroupAdding/AutoHiddenGroupRemoved events.
	/// </summary>
	public class AutoHiddenGroupEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonAutoHiddenGroup _autoHiddenGroup;
        private KryptonDockingAutoHiddenGroup _element;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the AutoHiddenGroupEventArgs class.
		/// </summary>
        /// <param name="control">Reference to auto hidden group control instance.</param>
        /// <param name="element">Reference to docking auto hidden group element that is managing the control.</param>
        public AutoHiddenGroupEventArgs(KryptonAutoHiddenGroup control,
                                        KryptonDockingAutoHiddenGroup element)
		{
            _autoHiddenGroup = control;
            _element = element;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonAutoHiddenGroup control.
        /// </summary>
        public KryptonAutoHiddenGroup AutoHiddenGroupControl
        {
            get { return _autoHiddenGroup; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingAutoHiddenGroup that is managing the group.
        /// </summary>
        public KryptonDockingAutoHiddenGroup AutoHiddenGroupElement
        {
            get { return _element; }
        }
        #endregion
	}
}
