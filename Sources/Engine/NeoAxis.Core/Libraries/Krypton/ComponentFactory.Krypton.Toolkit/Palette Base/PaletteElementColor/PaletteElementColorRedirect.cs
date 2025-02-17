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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;

namespace Internal.ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for element color values.
	/// </summary>
    public class PaletteElementColorRedirect : PaletteElementColor
    {
        #region Instance Fields
        private PaletteElementColorInheritRedirect _redirect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteElementColorRedirect class.
		/// </summary>
        /// <param name="redirect">Source for inheriting values.</param>
        /// <param name="element">Element value.</param>
        /// <param name="needPaint">Delegate for notifying changes in value.</param>
        public PaletteElementColorRedirect(PaletteRedirect redirect,
                                           PaletteElement element,
                                           NeedPaintHandler needPaint)
            : base(null, needPaint)
		{
            // Setup inheritence to recover values from the redirect instance
            _redirect = new PaletteElementColorInheritRedirect(redirect, element);
            SetInherit(_redirect);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public virtual void SetRedirector(PaletteRedirect redirect)
        {
            _redirect.SetRedirector(redirect);
        }
        #endregion
    }
}
