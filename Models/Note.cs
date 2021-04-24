using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace notes_api.Models
{
    public class Note
    { 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }
    }
}
