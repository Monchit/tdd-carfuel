﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFuel.Models
{
    public class FillUp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Range(0,99999)]
        public int Odometer { get; set; }
        [Range(0,999)]
        public double Liters { get; set; }

        public bool IsFull { get; set; }
        public bool IsForgot { get; set; }

        public FillUp NextFillUp { get; set; }
        public FillUp PreviousFillUp { get; set; }

        public FillUp()
            : this(0, 0, true)
        {
            // IsFull = true;
        }

        public FillUp(int odometer, double liters, bool isFull = true)
        {
            this.Odometer = odometer;
            this.Liters = liters;
            this.IsFull = isFull;
        }

        public double? KilometersPerLiter
        {
            get
            {
                if (this.NextFillUp == null || this.NextFillUp.IsForgot)
                {
                    return null;
                }
                var kml = (NextFillUp.Odometer - this.Odometer)
                          / NextFillUp.Liters;

                return Math.Round(kml, 1, MidpointRounding.AwayFromZero);
            }
        }

    }
}
