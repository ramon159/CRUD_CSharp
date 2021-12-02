using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    //Create read update delete
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private Context db;

        public UserController()
        {
            db = new Context();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(db.Users.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("User not found");
            }
        }

        [HttpPost]

        public IActionResult Store(string name, int age)
        {
            User user = new();
            user.Name = name;
            user.Age = age;

            db.Users.Add(user);

            db.SaveChanges();
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            var findedUser = db.Users.Find(user.Id);

            if (findedUser != null)
            {
                if (user.Name != null)
                {
                    findedUser.Name = user.Name;
                }

                if (user.Age != 0)
                {
                    findedUser.Age = user.Age;
                }

                db.SaveChanges();
                return Ok(findedUser);
            }
            else
            {
                return BadRequest("user not found");
            }



        }

        [HttpDelete("{id}")]
        public IActionResult Destroy(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Ok(user);
            }
            else
            {
                return BadRequest("User not found");
            }
        }
    }
}
