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

namespace Internal.ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for palette ribbon group label text states.
	/// </summary>
    public class KryptonPaletteRibbonGroupLabelText : KryptonPaletteRibbonGroupBaseText
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupLabelText class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupLabelText(PaletteRedirect redirect,
                                                  NeedPaintHandler needPaint)
            : base(redirect, PaletteRibbonTextStyle.RibbonGroupLabelText, needPaint)
		{
        }
        #endregion
    }
}
