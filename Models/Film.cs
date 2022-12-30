using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todoItemProject.Models
{
    
    public class Film
    {
        [Key]
        public int IdFilm { get; set; }

        public string nomfilm { get; set; }

        public string realisateurfilm { get; set; } = string.Empty;

        public int dureefilm { get; set; }
        public int anneesortie { get; set; }
        
        //categoryId attribute is a foreign key
        
        public int categoryId { get; set; }

        //category attribute is a navigation property
        public Category? category { get; set; }



    }
}
