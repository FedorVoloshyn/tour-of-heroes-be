using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourOfHeroes
{
    interface IDataContextFactory
    {
        IDataContext CreateContext();
    }
}
