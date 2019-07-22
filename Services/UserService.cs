using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ClickerAPI.Models;
using ClickerAPI.Models.Dao;
using ClickerAPI.Models.Dto;

namespace ClickerAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserDao> _users;

        private readonly UpgradesService _upgradeService;
        public UserService(IClickerDatabaseSettings settings, UpgradesService upgradesService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserDao>(settings.UsersCollectionName);
            _upgradeService = upgradesService;
        }

        public List<UserDao> Get() =>
            _users.Find(user => true).ToList();

        public UserDao Get(string id) =>
            _users.Find<UserDao>(user => user.Id == id).FirstOrDefault();

        public bool CheckIfValid(string username, string password)
        {
            var user = _users.Find<UserDao>(userIn => userIn.Username == username).FirstOrDefault();
            if (user == null || user.Username != username || user.Password != password)
            {
                return false;
            }
            return true;
        }

        public UserDao GetByUsername(string username) =>
            _users.Find<UserDao>(user => user.Username == username).FirstOrDefault();

        public bool Create(UserDto user)
        {
            UserDao userToDb = new UserDao();
            if (this.GetByUsername(user.Username) != null)
            {
                return false;
            }
            else
            {
                userToDb.Username = user.Username;
                userToDb.Password = user.Password;
                userToDb.Token = null;
                userToDb.Statistics = new StatisticsDao();
                userToDb.Statistics.UpgradeLevels = new List<UpgradeLvlsDao>();
                for (var i = 0; i < _upgradeService.Length(); i++)
                {
                    userToDb.Statistics.UpgradeLevels.Add(new UpgradeLvlsDao(i, 0));
                }
                _users.InsertOne(userToDb);
                return true;
            }
        }
        public void Update(string username, UserDao userIn)
        {
            _users.ReplaceOne(user => user.Username == username, userIn);
        }   

        public void Remove(UserDao userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);

        public void AddTokenToUser(string username, string token)
        {
            UserDao userTo = this.GetByUsername(username);
            userTo.Token = token;
            _users.ReplaceOne(user => user.Username == username, userTo);
        }
    }
}
