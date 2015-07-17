using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit; // (2)
using CarFuel.Models; 

namespace CarFuel.Facts.Models
{
    public class CarFacts // * Add public (1)
    {
        public class GeneralUsage
        {
            [Fact]
            public void NewCar_HasCorrectInitialValues()
            {
                var c = new Car();

                c.Make = "Honda";
                c.Model = "Accord";

                Assert.Equal("Honda", c.Make);
                Assert.Equal("Accord", c.Model);
                Assert.Equal(0, c.FillUps.Count());
            }
        }

        public class AddFillUpMethod
        {
            [Fact]
            public void CanAddNewFillUp()
            {
                var c = new Car();

                FillUp f = c.AddFillUp(odometer: 1000, liters: 50.0, isFull: true);

                //Check Method FillUps is OK
                Assert.Equal(1, c.FillUps.Count());

                //Check Return value
                Assert.Equal(1000, f.Odometer);
                Assert.Equal(50.0, f.Liters);
                Assert.True(f.IsFull);
            }

            [Fact]
            public void AddTwoFillUps()
            {
                var c = new Car();

                // add f1
                FillUp f1 = c.AddFillUp(odometer: 500, liters: 50.0, isFull: true);
                // add f2
                FillUp f2 = c.AddFillUp(odometer: 1000, liters: 20.0, isFull: true);

                //Assert that c has 2 file ups.
                Assert.Equal(2, c.FillUps.Count());
                //Assert that f1.NextFillUp is f2 -> use .same
                Assert.Same(f2, f1.NextFillUp);
                Assert.Same(f1, f2.PreviousFillUp);
            }

            [Fact]
            public void AddThreeFillUps()
            {
                var c = new Car();

                // add f1
                FillUp f1 = c.AddFillUp(odometer: 500, liters: 50.0, isFull: true);
                // add f2
                FillUp f2 = c.AddFillUp(odometer: 1000, liters: 20.0, isFull: true);
                // add f3
                FillUp f3 = c.AddFillUp(odometer: 800, liters: 30.0, isFull: true);

                //Assert that c has 3 file ups.
                Assert.Equal(3, c.FillUps.Count());
                //Assert that f1.NextFillUp is f2 -> use .Same
                Assert.Same(f1.NextFillUp, f2);
                //Assert that f2.NextFillUp is f3 -> use .Same
                Assert.Same(f2.NextFillUp, f3);
            }
        }

        public class AverageKilometersPerLiterProperty
        {
            [Fact]
            public void NoFillUp_IsNull()
            {
                var c = new Car();

                Assert.Null(c.AverageKilometersPerListes);
            }

            [Fact]
            public void SingleFillUp_IsNull()
            {
                var c = new Car();
                FillUp f1 = c.AddFillUp(odometer: 1000, liters: 50.0, isFull: true);

                Assert.Null(c.AverageKilometersPerListes);
            }

            [Fact]
            public void TwoFillUps()
            {
                var c = new Car();

                FillUp f1 = c.AddFillUp(odometer: 1000,
                                       liters: 50.0,
                                       isFull: true);

                FillUp f2 = c.AddFillUp(odometer: 1600,
                                       liters: 60.0,
                                       isFull: true);

                Assert.Equal(10.0, c.AverageKilometersPerListes);
            }

            [Fact]
            public void ThreeFillUps()
            {
                var c = new Car();

                FillUp f1 = c.AddFillUp(odometer: 1000,
                                       liters: 50.0,
                                       isFull: true);

                FillUp f2 = c.AddFillUp(odometer: 1600,
                                       liters: 60.0,
                                       isFull: true);

                FillUp f3 = c.AddFillUp(odometer: 2000,
                                       liters: 50.0,
                                       isFull: true);

                Assert.Equal(9.1, c.AverageKilometersPerListes);
            }

            [Fact]
            public void CanAvgKmPerLiter()
            {
                var c = new Car();

                // add f1
                FillUp f1 = c.AddFillUp(odometer: 1000, liters: 50.0, isFull: true);
                // add f2
                FillUp f2 = c.AddFillUp(odometer: 1600, liters: 60.0, isFull: true);
                // add f3
                FillUp f3 = c.AddFillUp(odometer: 2000, liters: 50.0, isFull: true);

                double? kml = c.AverageKilometersPerListes;

                Assert.Equal(9.1, kml);
            }
        }

        public class AverageKml_ForgotFillUps
        {
            [Fact]
            public void OneBlock()
            {
                var c = new Car();

                c.AddFillUp(odometer: 1000, liters: 50.0, isFull: true);
                c.AddFillUp(odometer: 1600, liters: 60.0, isFull: true, isForgot: true);
                c.AddFillUp(odometer: 2000, liters: 50.0, isFull: true);
                c.AddFillUp(odometer: 2600, liters: 60.0, isFull: true);
                c.AddFillUp(odometer: 3500, liters: 50.0, isFull: true, isForgot: true);

                double? kml = c.AverageKilometersPerListes;

                Assert.Equal(9.1, kml);
            }

            [Fact]
            public void TwoBlock()
            {
                var c = new Car();

                c.AddFillUp(1000, 50);
                c.AddFillUp(1600, 60);
                c.AddFillUp(2000, 50);

                c.AddFillUp(4000, 50, isForgot: true);
                c.AddFillUp(4600, 50);
                c.AddFillUp(5000, 50);

                var kml = c.AverageKilometersPerListes;

                Assert.Equal(9.6, kml);
            }
        }
    }
}
