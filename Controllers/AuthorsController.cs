using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fisher.Bookstore.Api.Data;
using Fisher.Bookstore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fisher.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly BookstoreContext db;
        public AuthorsController(BookstoreContext db)
        {
            this.db = db;

            if (this.db.Authors.Count() == 0)
            {
                this.db.Authors.Add(new Author {
                    Id = 0001,
                    Name = "Lt. Mike Murphy"
                });

                this.db.Authors.Add(new Author {
                    Id = 0002,
                    Name = "Staff Sgt. Mark Divine"
                });

                this.db.SaveChanges();
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.Authors);
        }

        [HttpGet("{Id}", Name="GetAuthor")]
        public IActionResult GetById(int Id)
        {
            var Author = db.Authors.Find(Id);

            if(Author == null)
            {
                return NotFound();
            }

            return Ok(Author);
        }
        [HttpPost]
        public IActionResult Post([FromBody]Author Author)
        {
            if(Author == null)
            {
                return BadRequest();
            }

            this.db.Authors.Add(Author);
            this.db.SaveChanges();

            return CreatedAtRoute("GetAuthor", new {Id = Author.Id}, Author);
        }
        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody]Author newAuthor)
        {
            if (newAuthor == null || newAuthor.Id != Id)
            {
                return BadRequest();
            }
            var currentAuthor = this.db.Authors.FirstOrDefault(x => x.Id == Id);

            if (currentAuthor == null)
            {
                return NotFound();
            }

        
            this.db.Authors.Update(currentAuthor);
            this.db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var author = this.db.Authors.FirstOrDefault(x => x.Id == Id);

            if (author == null)
            {
                return NotFound();
            }

            this.db.Authors.Remove(author);
            this.db.SaveChanges();

            return NoContent();
        }
    }
}
