using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassePartoutApi.data_access;
using PassePartoutApi.Infrastracture;
using PassePartoutApi.Models;

namespace PassePartoutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {
        private readonly UtenteDbContext _utenteDbContext;  
        private readonly IPasswordHasher _passwordHasher; //hashing psw per inserimento nel db
        public UtenteController(UtenteDbContext utenteDbContext, IPasswordHasher passwordHasher) 
        { 
            _utenteDbContext = utenteDbContext;
            _passwordHasher = passwordHasher;   
        }

        //API

        [HttpGet]
        public async Task<IActionResult> GetUtenti()
        {
            //Nessun reale utilizzo nell'applicativo quanto piu di debugging
            var utenti = await _utenteDbContext.Utente.ToListAsync();
            return Ok(utenti);
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody]Utente utenteRequest)
        {
            try
            {
                utenteRequest.Id = Guid.NewGuid();     //creo un id univoco per inserimento db
                utenteRequest.Password = _passwordHasher.Hash(utenteRequest.Password);
                await _utenteDbContext.Utente.AddAsync(utenteRequest);
                await _utenteDbContext.SaveChangesAsync();
                Console.WriteLine(utenteRequest.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }

            return Ok(utenteRequest);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginAuth loginRequest)
        {
            var utenteResponse = new Utente();
            try
            {
                utenteResponse = await _utenteDbContext.Utente.FirstOrDefaultAsync(x => x.Email == loginRequest.Email);

                //Per funzionare correttamente al register dovrei controllare se la mail inserita esiste gia'
                if (utenteResponse == null || !_passwordHasher.Verify(utenteResponse.Password, loginRequest.Password))
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(utenteResponse);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var utente = new Utente();
            try
            {
                utente = await _utenteDbContext.Utente.FirstOrDefaultAsync(x => x.Id == id);
                if (utente == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(utente);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, Utente updateUtenteRequest)
        {
            var utente = new Utente();
            try
            {
                utente = await _utenteDbContext.Utente.FirstOrDefaultAsync(x => x.Id == id);
                if (utente == null)
                {
                    return NotFound();
                }

                utente.Nome = updateUtenteRequest.Nome;
                utente.Cognome = updateUtenteRequest.Cognome;
                utente.Email = updateUtenteRequest.Email;
                utente.Citta = updateUtenteRequest.Citta;
                utente.Password = _passwordHasher.Hash(updateUtenteRequest.Password);
                await _utenteDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(utente);
        }
    }
}
