using MongoDB.Driver;
using notes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notes_api.Services
{
    public class NoteService
    {
        private readonly IMongoCollection<Note> _note;

        public NoteService(INotesDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _note = database.GetCollection<Note>(settings.NotesCollectionName);
        }

        public List<Note> Get() => _note.Find(note => true).ToList();

        public Note Get(String id) => _note.Find<Note>(note => note.Id == id).FirstOrDefault();

        public List<Note> GetByTitle(String title) => _note.Find(note => note.Title.Contains(title)).ToList();

        public Note Create(Note note) {
            _note.InsertOne(note);
            return note;
        }

        public void Update(string id, Note noteIn) => _note.ReplaceOne(note => note.Id == id, noteIn);

        public void Remove(Note noteIn) => _note.DeleteOne(note => note.Id == noteIn.Id);

        public void Remove(string id) => _note.DeleteOne(note => note.Id == id);

    }
}
