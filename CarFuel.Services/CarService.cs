using CarFuel.DataAccess.Context;
using CarFuel.DataAccess.Repositories;
using CarFuel.Models;
using GFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFuel.Services
{
    public class CarService : AppServiceBase<Car>
    {

        #region Service<T>
        public override IRepository<Car> Repository { get; set; }

        public override Car Find(params object[] keys)
        {
            Guid key1 = (Guid)keys[0];
            return Query(x => x.Id == key1).SingleOrDefault();
        }
        #endregion

    }
}