﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Wpf.NavigationService.Tests.App.Data
{
    public class DesignDataService : IDataService
    {
        public string GetName()
        {
            return "Sample Name, Jr.";
        }
    }
}
