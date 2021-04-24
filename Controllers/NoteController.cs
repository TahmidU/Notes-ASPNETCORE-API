using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using notes_api.Models;
using notes_api.Services;
using Microsoft.AspNetCore.Cors;

namespace notes_api.Controllers
{
    [ApiController]
    [EnableCors("_reactClient")]
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;

        private readonly NoteService _noteService;

        public NoteController(ILogger<NoteController> logger, NoteService noteService){
            _logger = logger;
            _noteService = noteService;
        }

        [EnableCors("_reactClient")]
        [HttpGet]
        public ActionResult<List<Note>> Get([FromQuery(Name = "title")] string title) {

            List<Note> notes;

            if (title != null)
                notes = _noteService.GetByTitle(title);
            else
                notes = _noteService.Get();

            if (notes == null)
                return NotFound();

            return notes;

        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Note> GetById(string id) {
            
            var note = _noteService.Get(id);

            if (note == null)
                return NotFound();

            return note;

        }

        [HttpPost]
        public ActionResult<Note> Create(Note note) {
            _noteService.Create(note);

            return CreatedAtRoute("Get", new { id = note.Id.ToString() }, note);
        }

        [HttpPut]
        public IActionResult Update(string id, Note noteIn){
            var note = _noteService.Get(id);

            if (note == null)
                return NotFound();

            _noteService.Update(id, noteIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id){
            var note = _noteService.Get(id);

            if (note == null)
                return NotFound();

            _noteService.Remove(note.Id);

            return NoContent();
        }
    }
}
