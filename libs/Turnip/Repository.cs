using System;
using System.IO;
using System.Linq;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Turnip
{
    public class Repository : IRepository<Player>
    {
        private readonly string _dbFilePath;
        private const string _tableName = "Players";

        public Repository(IConfiguration configuration)
        {
            _dbFilePath = Directory.GetCurrentDirectory() + configuration.GetSection("LiteDb")["RepositoryFilePath"];
        }

        public IDataSet<Player> GetData()
        {
            var db = new LiteDatabase(_dbFilePath);
            return new DataSet(db);
        }

        private class DataSet : IDataSet<Player>
        {
            private readonly LiteDatabase _db;
            private readonly ILiteCollection<Player> _collection;

            public DataSet(LiteDatabase db)
            {
                _db = db;
                _collection = db.GetCollection<Player>(_tableName);
            }

            public void Add(Player item)
            {
                _collection.Insert(item);
            }

            public void DeleteWhere(Func<Player, bool> predicate)
            {
                _collection.DeleteMany(player => predicate(player));
            }

            public void Dispose()
            {
                _db.Dispose();
            }

            public Player[] GetAll()
            {
                return _collection.FindAll().ToArray();
            }

            public void Update(Player item)
            {
                _collection.Update(item);
            }
        }
    }
}