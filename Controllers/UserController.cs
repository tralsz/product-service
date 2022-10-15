using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using product_service.config;
using product_service.Entity;
using System.Runtime.InteropServices;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace product_service.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
// [Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly Learn_DBContext _DBContext;
    private readonly JwtSettings jwtSettings;
    public UserController(Learn_DBContext context, IOptions<JwtSettings> options)
    {
         _DBContext = context;
        this.jwtSettings = options.Value;
        
    }



    // private readonly 
    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserCred userCred){
        //products products =  _DBContext.products.Where(x => x.name == userCred.username).First();
        var user = await _DBContext.users.FirstOrDefaultAsync(x => x.username == userCred.username && x.password == userCred.password);
        if (user == null)
        {
            return Unauthorized("Wrong Login Information");
        }

        // Generate Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(this.jwtSettings.securitykey);
        var tokenDesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            new Claim[] { new Claim(ClaimTypes.Name, user.username) } ),
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
        };
        var secToken = tokenHandler.CreateToken(tokenDesc);
        string userToken = tokenHandler.WriteToken(secToken);
        return Ok(userToken);
    }

    //[HttpPost("getAll")]
    //public string getAll()
    //{
    //    Console.Write("yes");
    //    return "string";
    //}
    
}