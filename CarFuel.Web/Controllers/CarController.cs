﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CarFuel.Models;
using CarFuel.Services;

namespace CarFuel.Web.Controllers
{
    public class CarController : Controller
    {
        //
        // GET: /Car/
        private CarService s = new CarService();

        public ActionResult Index()
        {
            var cars = s.GetAll();
            return View(cars);
        }

        public ActionResult AddCar()
        {
            var c = new Car();
            c.Make = "Toyota";
            c.Model = "Camry";
            c.Color = "White";

            var r = new Random();
            var odo = 1000;
            for (int i = 0; i < r.Next(3, 10); i++)
            {
                var isForgot = r.Next(100) < 5; // 5% forgot brain level.
                var liters = r.Next(300, 800) / 10.0; //30.0-80.0 float

                c.AddFillUp(odo, liters, isFull: true, isForgot: isForgot);

                odo += r.Next(300, 800);
            }

            s.Add(c);

            return RedirectToAction("Index");

        }

    }
}