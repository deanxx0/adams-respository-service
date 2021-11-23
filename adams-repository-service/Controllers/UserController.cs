using adams_repository_service.Data;
using adams_repository_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("user")]
        public ActionResult CreateUser(CreateUser createUser)
        {
            var user = new User(
                createUser.UserName,
                createUser.Password,
                createUser.UserClaim
                );
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return Ok(user);
        }

        [HttpGet("users")]
        public ActionResult<List<User>> GetAllUser()
        {
            var users = _appDbContext.Users.AsQueryable().ToList();
            return Ok(users);
        }

        [HttpGet("user/{username}")]
        public ActionResult GetUser(string username)
        {
            var user = _appDbContext.Users.AsQueryable().Where(x => x.UserName == username).FirstOrDefault();
            return Ok(user);
        }

        [HttpDelete("user/{username}")]
        public ActionResult DeleteUser(string username)
        {
            var user = _appDbContext.Users.AsQueryable().Where(x => x.UserName == username).FirstOrDefault();
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
            return Ok();
        }
    }
}
