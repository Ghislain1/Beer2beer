using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer2beer.Core.Entities;

public enum OrderState
{
    None,
    Pending,
    Canceled,
    Delivered
}
