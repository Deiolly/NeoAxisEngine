﻿// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
using System;
using System.Collections.Generic;
using NeoAxis.Editor;

namespace NeoAxis.Addon.ExampleEditorWinForms
{
	[AddToResourcesWindow( @"Addons\Example Editor WinForms" )]
	[EditorControl( typeof( ExampleEditorWinFormsWindow ) )]
	public class ExampleEditorWinForms : Component
	{
		public string ValueToDisplay { get; set; } = "Value to display";
	}
}
