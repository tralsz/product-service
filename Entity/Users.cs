using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace product_service.Entity
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string username { get; set; }
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string password { get; set; }


    }
}
