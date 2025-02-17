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
using System.IO;
using System.Xml;
using System.Drawing;
using System.Diagnostics;

namespace Internal.ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Event data for persisting extra data for a workspace.
	/// </summary>
	public class XmlLoadingEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonWorkspace _workspace;
        private XmlReader _xmlReader;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the XmlLoadingEventArgs class.
		/// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="xmlReading">Xml reader for persisting custom data.</param>
        public XmlLoadingEventArgs(KryptonWorkspace workspace,
                                   XmlReader xmlReading)
		{
            _workspace = workspace;
            _xmlReader = xmlReading;
		}
		#endregion

		#region Public
		/// <summary>
        /// Gets the workspace reference.
		/// </summary>
        public KryptonWorkspace Workspace
		{
            get { return _workspace; }
		}

        /// <summary>
        /// Gets the xml reader.
        /// </summary>
        public XmlReader XmlReader
        {
            get { return _xmlReader; }
        }
        #endregion
	}
}
