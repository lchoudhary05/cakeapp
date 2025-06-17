using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using CakeApp.Models;
using MongoDB.Driver;
using Settings;

namespace Services
{
    public class CakesService
    {
        private readonly IMongoCollection<Cakes> _cakeCollection;
        public CakesService(IOptions<CakeDatabaseSetting> cakesdbSetting)
        {
            var connection = new MongoClient(cakesdbSetting.Value.ConnectionString);
            var db = connection.GetDatabase(cakesdbSetting.Value.CakeDatabaseName);
            _cakeCollection = db.GetCollection<Cakes>(cakesdbSetting.Value.CakeCollectionName);
        }
        public async Task<List<Cakes>> GetAsync()
        {
            return await _cakeCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Cakes> GetAsync(string id)
        {
            return await _cakeCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task PostAsync(Cakes cake) =>

         await _cakeCollection.InsertOneAsync(cake);

        public async Task Updateasync(string id, Cakes cake)
        {
            await _cakeCollection.ReplaceOneAsync(item => item.Id == id, cake);
        }
        public async Task Deleteasync(string id)
        {
            await _cakeCollection.DeleteOneAsync(p => p.Id == id);
        }


    }
}