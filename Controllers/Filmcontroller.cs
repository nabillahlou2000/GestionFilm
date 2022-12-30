using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using todoItemProject.Data;
using todoItemProject.Models;

namespace todoItemProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Filmcontroller : ControllerBase
    {
        private readonly GestionDbContext context;

        public Filmcontroller(GestionDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Film>> GetFilms()
        {
            return await context.films.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Film), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilmById(int id)
        {
            var film = await context.films.FindAsync(id);
            return film == null ? NotFound() : Ok(film);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Film>> CreateFilm(Film film)
        {
            var filmdb = context.films.Where(d => d.nomfilm == film.nomfilm);
            if (!filmdb.IsNullOrEmpty()) return BadRequest("Movie already exists");

            if (film.categoryId != 0)
            {
                var category = await context.categories.FindAsync(film.categoryId);
                if (category == null)
                {
                    return BadRequest("Category not found");
                }
                film.category = category;
            }
            
            await context.films.AddAsync(film);
            await context.SaveChangesAsync();
            return Ok(film);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, Film film)
        {
            if (id != film.IdFilm) return BadRequest();

            if (film.categoryId != 0)
            {
                var category = await context.categories.FindAsync(film.categoryId);
                if (category == null)
                {
                    return BadRequest("Category not found");
                }
                film.category = category;
            }
            context.Entry(film).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Film>> DeleteProduct(int id)
        {
            var product = await context.films.FindAsync(id);
            if (product == null) return NotFound("Product not found");
            context.films.Remove(product);
            await context.SaveChangesAsync();
            return product;
        }

        /*****************************************Categories*************************************************/

        //get all categories
        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.categories.ToListAsync();
        }
        //get category by id
        [HttpGet("categories/{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await context.categories.FindAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        //delete category by id
        [HttpDelete("categories/{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await context.categories.FindAsync(id);

            //delete all product
            if (category == null) return NotFound("Category not found");
            context.categories.Remove(category);
            await context.SaveChangesAsync();
            return category;
        }

        //create category
        [HttpPost("categories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var categorydb = context.categories.Where(d => d.libellecategorie == category.libellecategorie);
            if (categorydb.Count() > 0)
            {
                return BadRequest("Category already exists");
            }
            await context.categories.AddAsync(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }

        //get category by id
        [HttpPut("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await context.categories.FindAsync(id);
            if (id != category.idcategory) return BadRequest();

            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }






    }
}
