using CakeApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Settings;

namespace Services
{
    public class HomeService
    {
        private readonly IMongoCollection<Users> _userCollection;
        public HomeService(IOptions<CakeDatabaseSetting> cakesdbSetting)
        {
            var connection = new MongoClient(cakesdbSetting.Value.ConnectionString);
            var db = connection.GetDatabase(cakesdbSetting.Value.CakeDatabaseName);
            _userCollection = db.GetCollection<Users>(cakesdbSetting.Value.UsersCollectionName);
        }

        public async Task CreateUserAsync(Users newUser)
        {
            await _userCollection.InsertOneAsync(newUser);

        }
        public async Task<Users> LoginUserAsync(Users user)
        {
            return await _userCollection.Find(p => p.Email == user.Email && p.Password == user.Password).FirstOrDefaultAsync();
        }
        public async Task<bool> CheckUserAsync(Users user)
        {
            var existingUser = await _userCollection.Find<Users>(p => p.Email == user.Email).FirstOrDefaultAsync();
            return existingUser != null;
            // var count = await _userCollection.CountDocumentsAsync(p => p.Email == user.Email);
            // return count > 0;
        }

    }
}