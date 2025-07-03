using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepoBase<User> _usre;
        public UserController(IRepoBase<User> usre)
        {
            _usre = usre;
        }
        private static string HashPassword(string Password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User? user)
        {
           
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");

            }
            User? user1 =await _usre.GetAsyncByParameter(user.Email);
            if (user1 != null)
            {
                ModelState.AddModelError("Error", "Email Exists , You Have Acount ");
                return Ok("Email Exists , You Have Acount ");
            }
            user.Password=HashPassword(user.Password);
            _usre.CreateAsync(user);
            return Ok("User Add ");

        }
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string ?Email,string ?Password )
        {
            try
            {

                if (!ModelState.IsValid|| Password == null||Email==null)
                {
                    ModelState.AddModelError("Error", "Not Valid Password Or Email ");
                    return Ok("INVALID Pass ");
                    
                }
                User? user1 =await _usre.GetAsyncByParameter(Email);
                if (user1 == null) {
                    ModelState.AddModelError("Error", "Not Valid Password Or Email ");
                    return Ok("INVALID Email ");
                }
                else
                {
                    var res=HashPassword(Password);
                    if (!user1.Password.Equals(res))
                    {
                        ModelState.AddModelError("Error", "Not Valid Password Or Email ");
                        return Ok("Invalid Email Or Password");
                    }
                }
                HttpContext.Session.SetString("Email", user1.Email);
            
                return Ok("Done");
            }
            catch
            {

               return Ok("Invalid Password Or Email");
            }
           
            
        }
        [HttpGet("Load User")]
        public async Task<IActionResult> Load()
        {
            string? Email = HttpContext.Session.GetString("Email");
            User? user1 = await _usre.GetAsyncByParameter(Email);
            return Ok(user1);
        }
       
        [HttpPost("Update")]
        public async Task<IActionResult> Update(User? user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Not Valid Update");
                return Ok("Not Valid Update");
            }
            user.Password = HashPassword(user.Password);
            string? Email = HttpContext.Session.GetString("Email");
            User olduser=await _usre.GetAsyncByParameter(Email);
            if (user.Email != Email) 
            {
                HttpContext.Session.SetString("Email",user.Email);
            }
            olduser.BDate=user.BDate;
            olduser.SacondName=user.SacondName;
            olduser.FirstName=user.FirstName;
            olduser.Email=user.Email;
            olduser.City=user.City;
            olduser.Password=user.Password;
            
            await _usre.UpdateAsync(olduser);
            return Ok("Up Done" );
        }
        [HttpGet("Forgot Password")]
        public async Task<IActionResult> Forgot(string? Email)
        {
            if (!ModelState.IsValid || Email == null)
            {
                ModelState.AddModelError("Error", "InValid Email");
                return Ok("InValid Email ");
            }
            User? user=await _usre.GetAsyncByParameter(Email);
            if (user == null)
            {
                ModelState.AddModelError("Error", "You Do not Have Acount ");
                return Ok("InValid Email ");
            }
            HttpContext.Session.SetString("Email", Email);
            Load();
            return Ok("Founed Acount");
            
        }
        [HttpGet("Get All User")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _usre.GetAsyncAll());
        }
        [HttpDelete("Delete User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _usre.DeleteAsync(id));
        }
    }

}
