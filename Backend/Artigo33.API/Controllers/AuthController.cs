using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Artigo33.API.Data;
using Artigo33.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace Artigo33.API.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly AppDbContext _context;


    public AuthController(AppDbContext context)
    {
        _context = context;
    }



    // ============================
    // CRIAR CONTA
    // POST /api/auth/register
    // ============================

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {

        var existe = await _context.Users
            .AnyAsync(x => x.Username == request.Username);


        if(existe)
        {
            return BadRequest(
                new {message="Usuário já existe"}
            );
        }



        var user = new User
        {

            Username = request.Username,

            Email = request.Email,

            PasswordHash = HashPassword(request.Password),

            CreatedAt = DateTime.UtcNow

        };


        _context.Users.Add(user);

        await _context.SaveChangesAsync();



        return Ok(new
        {
            message="Conta criada com sucesso",
            userId=user.Id
        });

    }





    // ============================
    // LOGIN
    // POST /api/auth/login
    // ============================

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var user = await _context.Users
            .FirstOrDefaultAsync(
                x=>x.Username == request.Username
            );


        if(user == null)
        {
            return Unauthorized(
                new {message="Usuário não encontrado"}
            );
        }



        var senha =
        HashPassword(request.Password);



        if(user.PasswordHash != senha)
        {
            return Unauthorized(
                new {message="Senha incorreta"}
            );
        }



        return Ok(new
        {
            message="Login realizado",

            userId=user.Id,

            username=user.Username

        });

    }





    private string HashPassword(string password)
    {

        using SHA256 sha = SHA256.Create();


        byte[] bytes =
            sha.ComputeHash(
                Encoding.UTF8.GetBytes(password)
            );


        StringBuilder builder =
            new StringBuilder();


        foreach(var b in bytes)
        {
            builder.Append(
                b.ToString("x2")
            );
        }


        return builder.ToString();

    }


}





public record RegisterRequest(

    string Username,

    string Email,

    string Password

);



public record LoginRequest(

    string Username,

    string Password

);