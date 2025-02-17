﻿//// *****************************************************************************
//// 
////  © Component Factory Pty Ltd 2012. All rights reserved.
////	The software and associated documentation supplied hereunder are the 
////  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
////  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
//// 
////
//// *****************************************************************************

//using System;
//using System.Text;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Drawing.Drawing2D;
//using System.Collections.Generic;
//using System.Windows.Forms;
//using System.Diagnostics;
//using Internal.ComponentFactory.Krypton.Toolkit;

//namespace Internal.ComponentFactory.Krypton.Ribbon
//{
//	/// <summary>
//	/// Draws a large image from a gallery.
//	/// </summary>
//    internal class ViewDrawRibbonGroupGalleryImage : ViewDrawRibbonGroupImageBase
                                              
//    {
//        #region Static Fields
//        private static readonly Size _largeSize = new Size(32, 32);
//        #endregion

//        #region Instance Fields
//        private KryptonRibbonGroupGallery _ribbonGallery;
//        #endregion

//        #region Identity
//        /// <summary>
//        /// Initialize a new instance of the ViewDrawRibbonGroupGalleryImage class.
//		/// </summary>
//        /// <param name="ribbon">Reference to owning ribbon control.</param>
//        /// <param name="ribbonGallery">Reference to ribbon group gallery definition.</param>
//        public ViewDrawRibbonGroupGalleryImage(KryptonRibbon ribbon,
//                                               KryptonRibbonGroupGallery ribbonGallery)
//            : base(ribbon)
//        {
//            Debug.Assert(ribbonGallery != null);

//            _ribbonGallery = ribbonGallery;
//        }        

//		/// <summary>
//		/// Obtains the String representation of this instance.
//		/// </summary>
//		/// <returns>User readable name of the instance.</returns>
//		public override string ToString()
//		{
//			// Return the class name and instance identifier
//            return "ViewDrawRibbonGroupGalleryImage:" + Id;
//		}
//        #endregion

//        #region Protected
//        /// <summary>
//        /// Gets the size to draw the image.
//        /// </summary>
//        protected override Size DrawSize 
//        {
//            get { return _largeSize; }
//        }

//        /// <summary>
//        /// Gets the image to be drawn.
//        /// </summary>
//        protected override Image DrawImage 
//        {
//            get { return _ribbonGallery.ImageLarge; }
//        }
//        #endregion
//    }
//}
