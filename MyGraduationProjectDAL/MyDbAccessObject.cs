using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGraduationProject.Models;
using MyGraduationProject.DataBase;

namespace MyGraduationProjectDAL
    {
    public class MyDbAccessObject : Core.DataAccessObject<Database.MyGraduationProje>
    {
        public MyDbAccessObject(Database database)
            : base(database)
        {
        }
        /*
        public GFTMarketDatabaseInstance(Database database)
                : base(database)
            {
            }
            */
        public override void Delete<Entity>(Entity dbObject)
            {
                if (dbObject.GetType().Name == "Reservation")
                {
                    _database.Transactions.Remove(dbObject.GetInstance<Reservation>());
                    _database.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            public override void Insert<Entity>(Entity dbObject)
            {
                switch (dbObject.GetType().Name)
                {
                    case "Transaction":
                        _database.Transactions.Add(dbObject.GetInstance<Reservation>());
                        _database.SaveChanges();
                        break;
                    case "Item":
                        _database.Items.Add(dbObject.GetInstance<Item>());
                        _database.SaveChanges();
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            public override Entity Read<Entity>(int entityId)
            {
                switch (typeof(Entity).Name)
                {
                    case "Reservation":
                        return (Entity)Convert.ChangeType(_database.Transactions.Find(entityId),
                            typeof(Reservation));

                    case "Item":
                        return (Entity)Convert.ChangeType(_database.Items.Find(entityId),
                            typeof(Item));
                    default:
                        throw new InvalidOperationException();
                }
            }

            public override void Update<Entity>(Entity dbObject)
            {
                switch (typeof(Entity).Name)
                {
                    case "Transaction":
                        _database.Transactions.Remove(_database.Transactions
                            .Find(dbObject.GetInstance<Reservation>().RESERVATION_ID));
                        _database.Transactions.Add(dbObject.GetInstance<Reservation>());
                        break;

                    case "Item":
                        _database.Items.Remove(_database.Items
                            .Find(dbObject.GetInstance<Item>().ITEM_ID));
                        _database.Items.Add(dbObject.GetInstance<Item>());
                        break;

                    default:
                        throw new InvalidOperationException();

                }
            }
        }
    }
