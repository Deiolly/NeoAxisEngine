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
	/// Implement storage for month calendar appearance states.
	/// </summary>
    public class PaletteMonthCalendarState : Storage
    {
        #region Instance Fields
        private PaletteTriple _paletteDay;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarState class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect)
            : this(redirect, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarState class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect,
                                         NeedPaintHandler needPaint) 
		{
            _paletteDay = new PaletteTriple(redirect.Day, needPaint);
        }
        #endregion

		#region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
            get
            {
                return _paletteDay.IsDefault;
            }
		}
		#endregion

        #region Day
        /// <summary>
        /// Gets access to the day appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining day appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Day
        {
            get { return _paletteDay; }
        }

        private bool ShouldSerializeContent()
        {
            return !_paletteDay.IsDefault;
        }
        #endregion
    }
}
