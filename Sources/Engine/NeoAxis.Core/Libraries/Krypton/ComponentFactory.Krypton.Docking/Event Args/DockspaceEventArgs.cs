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
using Internal.ComponentFactory.Krypton.Workspace;

namespace Internal.ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for a DockspaceAdding/DockspaceRemoved events.
	/// </summary>
	public class DockspaceEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonDockspace _dockspace;
        private KryptonDockingDockspace _element;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the DockspaceEventArgs class.
		/// </summary>
        /// <param name="dockspace">Reference to new dockspace control instance.</param>
        /// <param name="element">Reference to docking dockspace element that is managing the dockspace control.</param>
        public DockspaceEventArgs(KryptonDockspace dockspace,
                                  KryptonDockingDockspace element)
		{
            _dockspace = dockspace;
            _element = element;
		}
        #endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonDockspace control.
        /// </summary>
        public KryptonDockspace DockspaceControl
        {
            get { return _dockspace; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace control.
        /// </summary>
        public KryptonDockingDockspace DockspaceElement
        {
            get { return _element; }
        }
        #endregion
	}
}
