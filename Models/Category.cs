using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todoItemProject.Models
{
    public class Category
    {
        [Key]
        public int idcategory { get; set; }

        public string libellecategorie { get; set; } = string.Empty;

    }
}
