using MongoDB.Driver;
using NutritecComments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritecComments.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentService(ICommentsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _comments = database.GetCollection<Comment>(settings.CommentsCollectionName);
        }

        // get all comments
        public List<Comment> GetAll() =>
            _comments.Find(comment => true).ToList();

        // get all comments related to a patient in a respective day and meal
        public List<Comment> GetFilteredComments(string patientEmail, string day, string meal) =>
            _comments.Find<Comment>(comment => comment.PatientEmail == patientEmail && comment.Day == day && comment.Meal == meal).ToList();

        // post a comment
        public Comment Create(Comment comment)
        {
            _comments.InsertOne(comment);
            return comment;
        }
    }
}
