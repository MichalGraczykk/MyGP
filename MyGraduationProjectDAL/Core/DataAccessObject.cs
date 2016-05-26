using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGraduationProject.Models.Interfaces;

namespace MyGraduationProject.DataAccessLayer.Core
{
    public abstract class DataAccessObject<Database> : IDisposable
        where Database : DbContext
    {
        protected Database _database { get; set; }

        public DataAccessObject(Database database)
        {
            _database = database;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public abstract void Delete<Entity>(Entity dbObject)
            where Entity : IDatabaseEntity;

        public abstract void Insert<Entity>(Entity dbObject)
            where Entity : IDatabaseEntity;

        public abstract Entity Read<Entity>(int entityId)
            where Entity : IDatabaseEntity;

        public abstract void Update<Entity>(Entity dbObject)
            where Entity : IDatabaseEntity;

    }
}
