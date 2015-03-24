﻿using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
namespace LightSwitchApplication
{
    public partial class CreateNewDrawingPrefix
    {

        partial void CreateNewDrawingPrefix_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.DrawingPrefixProperty = new DrawingPrefix();
        }
    }
}