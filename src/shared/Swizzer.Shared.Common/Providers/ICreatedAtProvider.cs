﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Providers
{
    public interface ICreatedAtProvider
    {
        DateTime CreatedAt { get; set; }
    }
}
