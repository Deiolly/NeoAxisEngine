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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Internal.ComponentFactory.Krypton.Toolkit;

namespace Internal.ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Delegate used for hooking into a KryptonRibbonContext typed collection.
    /// </summary>
    public delegate void RibbonRecentDocHandler(object sender, TypedCollectionEventArgs<KryptonRibbonRecentDoc> e);

    /// <summary>
    /// Specialise the generic collection with type specific rules for recent document item accessor.
    /// </summary>
    public class KryptonRibbonRecentDocCollection : TypedCollection<KryptonRibbonRecentDoc>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided document name.
        /// </summary>
        /// <param name="name">Name of the recent document instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonRibbonRecentDoc this[string name]
        {
            get
            {
                // Search for an entry with the same text name as that requested.
                foreach (KryptonRibbonRecentDoc recentDoc in this)
                    if (recentDoc.Text == name)
                        return recentDoc;

                // Let base class perform standard processing
                return base[name];
            }
        }
        #endregion
    }
}
