using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritecComments.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string PatientEmail { get; set; }
        public string Day { get; set; }
        public string Meal { get; set; }
        public string CommentOwnerEmail { get; set; }
        public string CommentText { get; set; }
    }
}
