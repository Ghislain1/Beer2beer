using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beer2beer.Core.Entities;

public class Order : Base<int>
{
    public Contact Contact { get; set; } = null!;
}
