using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ClickerAPI.Models;
using ClickerAPI.Models.Dao;

namespace ClickerAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserDao> _users;
        public UserService(IClickerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserDao>(settings.UsersCollectionName);
        }

        public List<UserDao> Get() =>
            _users.Find(user => true).ToList();

        public UserDao Get(string id) =>
            _users.Find<UserDao>(user => user.Id == id).FirstOrDefault();

        public UserDao GetByUsername(string username) =>
            _users.Find<UserDao>(user => user.Username == username).FirstOrDefault();

        public UserDao Create(UserDao user)
        {
            _users.InsertOne(user);
            return user;
        }
        public void Update(string username, UserDao userIn)
        {
            _users.ReplaceOne(user => user.Username == username, userIn);
        }   

        public void Remove(UserDao userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}
