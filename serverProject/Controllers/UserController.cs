﻿using Microsoft.AspNetCore.Mvc;
using serverProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace serverProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
              new User { id = 1, name = "John Smith", mail = "john@example.com", password = "password1" },
        new User { id = 2, name = "Jane Doe", mail = "jane@example.com", password = "password2" },
        new User { id = 3, name = "Alice Johnson", mail = "alice@example.com", password = "password3" },
        new User { id = 4, name = "Bob Brown", mail = "bob@example.com", password = "password4" },
        new User { id = 5, name = "Emily Davis", mail = "emily@example.com", password = "password5" }
        };
        private static int counter = 0;
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return users.Find(u => u.id == id);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            value.id = ++counter;
            users.Add(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            var u = users.Find(u => u.id == id);
            if (u is null)
            {
                u.id = ++counter;
                users.Add(u);
            }
            else
            {
                u.name = value.name;
                u.mail = value.mail;
                u.password = value.password;
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var u = users.Find(u => u.id == id);
            users.Remove(u);
        }

        [HttpPost("login")]
        public string Login([FromBody] UserCredentials credentials)
        {
            var user = users.FirstOrDefault(u => u.mail == credentials.mail);
            if (user == null)
            {
                return "invalid_username";
            }
            else if (user.password == credentials.password)
            {
                return "";
            }

            else
            {
                return "the password inncorrect";
            }
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (users.Any(u => u.mail == newUser.mail))
            {
                return Conflict("User with this email already exists.");
            }

            newUser.id = ++counter;
            users.Add(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.id }, newUser);
        }
    }
}
public class UserCredentials
{
    public string mail { get; set; }
    public string password { get; set; }
}
