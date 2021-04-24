using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notes_api.Models
{
    public interface INotesDatabaseSettings 
    {
        public string NotesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public class NotesDatabaseSettings : INotesDatabaseSettings
    {
        public string NotesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
