using System.ComponentModel.DataAnnotations;

namespace MotorcycleCatalog.DTOs.AdminDTOs
{
    public class AuthDTO
    {
        public string Token { get; set; }

        public AuthDTO(string token)
        {
            Token = token;
        }
    }
}
