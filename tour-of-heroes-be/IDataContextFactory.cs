using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tour_of_heroes_be
{
    interface IDataContextFactory
    {
        IDataContext CreateContext();
    }
}
