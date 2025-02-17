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
    /// Event arguments for a FloatingWindowAdding/FloatingWindowRemoved event.
	/// </summary>
	public class FloatingWindowEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonFloatingWindow _floatingWindow;
        private KryptonDockingFloatingWindow _element;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the FloatingWindowEventArgs class.
		/// </summary>
        /// <param name="floatingWindow">Reference to floating window instance.</param>
        /// <param name="element">Reference to docking floating winodw element that is managing the floating window.</param>
        public FloatingWindowEventArgs(KryptonFloatingWindow floatingWindow,
                                       KryptonDockingFloatingWindow element)
		{
            _floatingWindow = floatingWindow;
            _element = element;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonFloatingWindow control.
        /// </summary>
        public KryptonFloatingWindow FloatingWindow
        {
            get { return _floatingWindow; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingFloatingWindow that is managing the dockspace.
        /// </summary>
        public KryptonDockingFloatingWindow FloatingWindowElement
        {
            get { return _element; }
        }
        #endregion
	}
}
