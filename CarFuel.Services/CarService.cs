using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarFuel.DataAccess;
using CarFuel.Models;
using CarFuel.DataAccess.Repositories;
using CarFuel.DataAccess.Context;

namespace CarFuel.Services
{
    public class CarService
    {
        private readonly CarRepository repo;//allow assign value by constructor method

        public CarService()
            : this(new CarRepository())
        {

        }

        public CarService(CarRepository repo)
        {
            this.repo = repo;
            this.repo.Context = new CarDb();
        }

        public IEnumerable<Car> GetAll()
        {
            return repo.Query(c => true).ToList();
        }

        public Car Add(Car item)
        {
            var c = repo.Add(item);
            repo.SaveChanges();
            return c;
        }
    }
}
