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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Internal.ComponentFactory.Krypton.Toolkit;

namespace Internal.ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws either a large or small image from a group button.
	/// </summary>
    internal class ViewDrawRibbonGroupButtonImage : ViewDrawRibbonGroupImageBase
                                              
    {
        #region Static Fields
        private static Size _smallSize = new Size(16, 16);
        private static Size _largeSize = new Size(32, 32);
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupButton _ribbonButton;
        private bool _large;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupButtonImage class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonButton">Reference to ribbon group button definition.</param>
        /// <param name="large">Show the large image.</param>
        public ViewDrawRibbonGroupButtonImage(KryptonRibbon ribbon,
                                              KryptonRibbonGroupButton ribbonButton,
                                              bool large)
            : base(ribbon)
        {
            Debug.Assert(ribbonButton != null);

            _ribbonButton = ribbonButton;
            _large = large;
        }

        static ViewDrawRibbonGroupButtonImage()
        {
            if (DpiHelper.Default.DpiScaleFactor >= 1.5)
            {
                float scale = DpiHelper.Default.DpiScaleFactor * 0.8f;
                _smallSize = DpiHelper.Default.ScaleValue(_smallSize, scale);
                _largeSize = DpiHelper.Default.ScaleValue(_largeSize, scale);
            }
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupButtonImage:" + Id;
		}
        #endregion

        #region Protected
        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected override Size DrawSize 
        {
            get
            {
                if (_large)
                    return _largeSize;
                else
                    return _smallSize;
            }
        }

        /// <summary>
        /// Gets the image to be drawn.
        /// </summary>
        protected override Image DrawImage 
        {
            get
            {
                if (_ribbonButton.KryptonCommand != null)
                {
                    if (_large)
                        return _ribbonButton.KryptonCommand.ImageLarge;
                    else
                        return _ribbonButton.KryptonCommand.ImageSmall;
                }
                else
                {
                    if (_large)
                        return _ribbonButton.ImageLarge;
                    else
                        return _ribbonButton.ImageSmall;
                }
            }
        }
        #endregion
    }
}
