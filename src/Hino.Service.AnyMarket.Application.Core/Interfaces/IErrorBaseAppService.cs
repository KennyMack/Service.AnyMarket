﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.Interfaces
{
    public interface IErrorBaseAppService
    {
        List<string> Errors { get; }
    }
}
