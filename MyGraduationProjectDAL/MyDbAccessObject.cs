using System;
using MyGraduationProject.Models;

namespace MyGraduationProjectDAL {
    public class MyDbAccessObject : Core.DataAccessObject<DatabaseContext>
    {
        public MyDbAccessObject(DatabaseContext database)
            : base(database)
        {
        }

        public override void Delete<Entity>(Entity dbObject)
            {
                if (dbObject.GetType().Name == "Reservation")
                {
                    _database.Reservations.Remove(dbObject.GetInstance<Reservation>());
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
                        _database.Reservations.Add(dbObject.GetInstance<Reservation>());
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
                        return (Entity)Convert.ChangeType(_database.Reservations.Find(entityId),
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
                        _database.Reservations.Remove(_database.Reservations
                            .Find(dbObject.GetInstance<Reservation>().RESERVATION_ID));
                        _database.Reservations.Add(dbObject.GetInstance<Reservation>());
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
