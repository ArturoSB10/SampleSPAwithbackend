using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleSPAwithbackend.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleSPAwithbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private static string adminToken = Guid.NewGuid().ToString("n").Substring(0, 10);
        private static string userToken = Guid.NewGuid().ToString("n").Substring(0, 10);

        private readonly SampleSpaContext db;

        public Users(SampleSpaContext context)
        {
            db = context;
        }

        // GET: api/<Users>
        [HttpGet]
        public ObjectResult Get(string token)
        {
            try
            {
                if (token.Equals(adminToken) || token.Equals(userToken))
                    return Ok(db.Users.ToList());

                return Unauthorized(new { StatusCode = 401, Message = "Unauthorized: Must login" });

            }
            catch (Exception ex)
            {
                LogError error = new LogError();
                error.Error = ex.Message;
                db.LogErrors.Add(error);
                return StatusCode(500, new { StatusCode = 500, Message = ex });
            }
        }

        
        [HttpPost("registerUsers")]
        public IActionResult Post([FromBody] UserToken users)
        {
            try
            {

                if (users.Token.Equals(adminToken))
                {
                    if (users.Users == null)
                        return BadRequest(new { StatusCode = 400, Message = "Unauthorized: Must include users" });

                    bool passwordValid;
                    List<User> ValidUsers = new List<User>();
                    List<User> InvalidUsers = new List<User>();

                    foreach (var user in users.Users)
                    {
                        passwordValid = Regex.IsMatch(user.Password, @"^(?=.*\d)(?=.*[\W_])(?=.*[A-Z]).*$");
                        if (user.UserName.Length >= 5 && passwordValid)
                        {
                            ValidUsers.Add(new User
                            {
                                UserName = user.UserName,
                                Password = user.Password,
                            });
                        }
                        else
                        {
                            InvalidUsers.Add(new User
                            {
                                UserName = user.UserName,
                                Password = user.Password
                            });
                        }
                    }

                    db.Users.AddRange(ValidUsers);
                    db.SaveChanges();

                    return Ok(new { ValidUsers = ValidUsers, InvalidUsers = InvalidUsers });
                }
                else
                {
                    return Unauthorized(new { StatusCode = 401, Message = "Unauthorized: Must be admin" });
                }
            }
            catch (Exception ex)
            {
                LogError error = new LogError();
                error.Error = ex.Message;
                db.LogErrors.Add(error);
                return StatusCode(500, new { StatusCode = 500, Message = ex });
            }
        }


        // PUT api/<Users>/5
        [HttpPost("login")]
        public ObjectResult Login(IFormCollection login)
        {

            

            try
            {
                string username = login["UserName"];
                string pass = login["Password"];

                var user = db.Users.FirstOrDefault(x => x.UserName == username && x.Password == pass);
                if (user != null) {

                    if (user.IsAdmin)
                        return Ok(new { StatusCode = 200, Token = adminToken });
                    else
                        return Ok(new { StatusCode = 200, Token = userToken });

                }

                return Unauthorized(new { StatusCode = 401, Message = "Unauthorized: Invalid credentials" });

            }
            catch (Exception ex)
            {
                LogError error = new LogError();
                error.Error = ex.Message;
                db.LogErrors.Add(error);
                return StatusCode(500, new { StatusCode = 500, Message = ex });
            }


        }

        [HttpPost("register")]
        public ObjectResult Register(IFormCollection newUser)
        {



            try
            {
                string username = newUser["UserName"];
                string pass = newUser["Password"];


                if (Regex.IsMatch(pass, @"^(?=.*\d)(?=.*[\W_])(?=.*[A-Z]).*$"))
                {

                    User user = new User();
                    user.UserName = username;
                    user.Password = pass;
                    db.Add(user);
                    db.SaveChanges();

                    return Ok(new { StatusCode = 200, Message = "Successful register" });
                        
                }

                return BadRequest(new { StatusCode = 400, Message = "Invalid format password" });

            }
            catch (Exception ex)
            {
                LogError error = new LogError();
                error.Error = ex.Message;
                db.LogErrors.Add(error);
                return StatusCode(500, new { StatusCode = 500, Message = ex });
            }


        }




    }
}
