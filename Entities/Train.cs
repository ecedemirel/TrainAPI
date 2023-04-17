using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrainAPI.Entities
{
    public class Train
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("From")]
        public string From { get; set; }

        [BsonElement("To")]
        public string To { get; set; }

        [BsonElement("Date")]
        public string Date { get; set; }
    }
}
