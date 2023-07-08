using Microsoft.AspNetCore.Identity;
using SAQAYA.EntityFramework.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAQAYA.DataAccess.Entities
{
    public partial class Users : BaseEntity
    {
        public Users()
        {

        }

        //[MaxLength(150)]
        //public string UserName { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string? FirstName { get; set; }

        [MaxLength(150)]
        public string? LastName { get; set; }

        public bool MarkrtingConsent { get; set; }

    }
}
