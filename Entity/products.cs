using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace product_service.Entity
{
    public class products
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]

        [Column(TypeName = "varchar(250)")]
        [Index(nameof(name), IsUnique = true)]
        public string name { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string ImageUrl { get; set; }

    }
}
