using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]

    // [Route("[controller]s")]
    public class CarController : ControllerBase
    {
        //     //readonly ifadeler sadece contructorlar icinde set edilebilirler
        //     // private readonly BookStoreDbContext _context; // Startup.cs'de servislere ekledigimiz BookStoreDbContext in constructor yardimiyla bir ornegini olusturduk (uygulama icerisinden degistiliemesin diye priavte and readonly)

        //     public BookController(BookStoreDbContext context)
        //     {
        //         // _context = context;//yukardaki private context'i artik burda kullanabiliriz. (bir kere burda set ettik ve uygulama icinde bir daha degistirelemez)
        //     }


        private static List<Car> CarList = new List<Car>()
        {
            new Car{
               Id=1,
                NumberPlate="NW5I STY",
                ModelCode=1, //personal growth et cetera
                ModelYear=new DateTime(2001,06,12),
                UserId=3,
            },

            new Car{
               Id=2,
                NumberPlate="P3 FIX",
                ModelCode=1, //personal growth et cetera
                ModelYear=new DateTime(2001,06,12),
                UserId=3,
            },
            new Car{
                Id=3,
                NumberPlate="SUF I23X",
                ModelCode=1, //personal growth et cetera
                ModelYear=new DateTime(2001,06,12),
                UserId=3,
            },
        };

        [HttpGet]
        public List<Car> GetCars()
        {
            var carList = CarList.OrderBy(x => x.Id).ToList<Car>();

            return carList;
        }

        [HttpGet("{id}")]
        public Car GetById(int id)//route'tan

        {
            var car = CarList.Where(car => car.Id == id).SingleOrDefault();
            return car;
        }

        // [HttpGet]

        // public Book Get([FromQuery] string id)
        // {//query'den
        //     var car = carList.Where(car => car.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return car;
        // }


        //---------------POST-----------------
        [HttpPost]
        public IActionResult AddBook([FromBody] Car newCar)
        {
            var car = CarList.SingleOrDefault(x => x.Id == newCar.Id);

            if (car is not null)
            {
                return BadRequest();
            }

            CarList.Add(newCar);
            // _context.SaveChanges();
            return Ok();
        }

        //---------------PUT-----------------

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Car updatedCar)
        {
            var car = CarList.SingleOrDefault(x => x.Id == id);

            if (car is null)
            {
                return BadRequest();
            }

            car.NumberPlate = updatedCar.NumberPlate != default ? updatedCar.NumberPlate : car.NumberPlate;
            car.ModelCode = updatedCar.ModelCode != default ? updatedCar.ModelCode : car.ModelCode;
            car.ModelYear = updatedCar.ModelYear != default ? updatedCar.ModelYear : car.ModelYear;
            car.UserId = updatedCar.UserId != default ? updatedCar.UserId : car.UserId;

            //_context.SaveChanges();
            return Ok();
        }

        //---------------DELETE-----------------
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = CarList.SingleOrDefault(x => x.Id == id);

            if (car is null)
                return BadRequest();

            CarList.Remove(car);
            //  _context.SaveChanges();
            return Ok();

        }
        [HttpDelete]
        public IActionResult DeleteAllCars()
        {
            var carList = CarList.OrderBy(x => x.Id).ToList<Car>(); ;

            if (carList is null)
                return BadRequest();

            CarList.Clear();
            //  _context.SaveChanges();
            return Ok();

        }
    }
}