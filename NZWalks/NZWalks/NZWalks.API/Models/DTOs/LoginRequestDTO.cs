using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Models.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
