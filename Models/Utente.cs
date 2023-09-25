using System.ComponentModel.DataAnnotations;

namespace PassePartoutApi.Models
{
    public class Utente
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Citta { get; set; }

    }
}
