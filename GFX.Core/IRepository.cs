﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GFX.Core
{
    public interface IRepository //Change class to interface
    {
        DbContext Context { get; set; }
    }
}
