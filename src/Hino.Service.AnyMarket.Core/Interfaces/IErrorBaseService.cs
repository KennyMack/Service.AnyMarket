using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Core.Interfaces
{
    public interface IErrorBaseService
    {
        List<string> Errors { get; set; }
    }
}
