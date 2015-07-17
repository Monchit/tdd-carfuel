using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.Models;
using CarFuel.Services;

using Xunit;//(3) add this using

namespace CarFuel.Facts.Services
{
    public class CarServiceFacts//(1) add public in class
    {
        public class Add//(2) add class in class
        {
            [Fact]
            public void ValidMake_AddOk()
            {
                using (var app = new App(testing: true))
                {
                    var c = new Car();
                    c.Make = "Honda"; // valid make

                    app.Cars.Add(c);
                    app.Cars.SaveChanges();

                    var n = app.Cars.All().Count();

                    Assert.Equal(1, n);
                }
            }

            [Fact]
            public void InvalidMake_AddFailed()
            {
                using (var app = new App(testing: true))
                {
                    var c = new Car();
                    c.Make = "Google"; // invalid make

                    app.Cars.Add(c);
                    app.Cars.SaveChanges();

                    var n = app.Cars.All().Count();

                    Assert.Equal(0, n);
                }
            }
        }
    }
}
