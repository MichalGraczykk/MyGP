using MyGraduationProject.Models;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyGraduationProject.DataAccessLayer

{
    public class MyGPDatbaseInstance : Core.DataAccessObject<MyGPDatabaseContext>
    {
        public MyGPDatbaseInstance(MyGPDatabaseContext database)
            : base(database)
        {
        }

        public override void Delete<Entity>(Entity dbObject)
        {

            switch (typeof(Entity).Name)
            {
                case "Resevation":
                    _database.Reservations.Remove(dbObject.GetInstance<Reservation>());
                    _database.SaveChanges();
                    break;

                case "User":
                    _database.Users.Remove(dbObject.GetInstance<User>());
                    _database.SaveChanges();
                    break;

                case "UsersAdress":
                    _database.UsersAdresses.Remove(dbObject.GetInstance<UsersAdress>());
                    _database.SaveChanges();
                    break;

                default:
                    throw new ArgumentException();
            }


        }

        public override void Insert<Entity>(Entity dbObject)
        {
            switch (dbObject.GetType().Name)
            {
                case "Reservation":
                    _database.Reservations.Add(dbObject.GetInstance<Reservation>());
                    _database.SaveChanges();
                    break;

                case "Item":
                    _database.Items.Add(dbObject.GetInstance<Item>());
                    _database.SaveChanges();
                    break;

                case "User":
                    _database.Users.Add(dbObject.GetInstance<User>());
                    _database.SaveChanges();
                    break;

                case "UsersAdress":
                    _database.UsersAdresses.Add(dbObject.GetInstance<UsersAdress>());
                    _database.SaveChanges();
                    break;

                case "States":
                    _database.States.Add(dbObject.GetInstance<State>());
                    _database.SaveChanges();
                    break;

                case "Role":
                    _database.Roles.Add(dbObject.GetInstance<Role>());
                    _database.SaveChanges();
                    break;

                case "ReservationStatus":
                    _database.ReservationStatuses.Add(dbObject.GetInstance<ReservationStatus>());
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

                case "ReservationStatus":
                    return (Entity)Convert.ChangeType(_database.ReservationStatuses.Find(entityId),
                        typeof(ReservationStatus));

                case "Role":
                    return (Entity)Convert.ChangeType(_database.Roles.Find(entityId),
                        typeof(Role));

                case "State":
                    return (Entity)Convert.ChangeType(_database.States.Find(entityId),
                        typeof(State));

                case "User":
                    return (Entity)Convert.ChangeType(_database.Users.Find(entityId),
                        typeof(User));

                case "UsersAdress":
                    return (Entity)Convert.ChangeType(_database.UsersAdresses.Find(entityId),
                        typeof(UsersAdress));

                default:
                    throw new InvalidOperationException();
            }
        }

        public override void Update<Entity>(Entity dbObject)
        {
            switch (typeof(Entity).Name)
            {
                case "Reservation":
                    var reservation = _database.Reservations
                        .Where(o => o.RESERVATION_ID.ToString() == typeof(Reservation)
                        .GetProperty("RESERVATION_ID")
                        .GetValue(dbObject).ToString());

                    reservation = (IQueryable<Reservation>)dbObject.GetInstance<Reservation>();
                    _database.SaveChanges();
                    break;

                case "Item":
                    var item = _database.Items
                    .Where(o => o.ITEM_ID.ToString() == typeof(Item)
                    .GetProperty("ITEM_ID")
                    .GetValue(dbObject).ToString());

                    item = (IQueryable<Item>)dbObject.GetInstance<Item>();
                    break;

                case "ReservationStatus":
                    var reservationStatus = _database.ReservationStatuses
                    .Where(o => o.STATUS_ID.ToString() == typeof(ReservationStatus)
                    .GetProperty("STATUS_ID")
                    .GetValue(dbObject).ToString());

                    reservationStatus = (IQueryable<ReservationStatus>)dbObject.GetInstance<ReservationStatus>();
                    break;

                case "Role":
                    var role = _database.Roles
                    .Where(o => o.ROLE_ID.ToString() == typeof(Role)
                    .GetProperty("ROLE_ID")
                    .GetValue(dbObject).ToString());

                    role = (IQueryable<Role>)dbObject.GetInstance<Role>();
                    break;

                case "User":
                    var user = _database.Users
                    .Where(o => o.USER_ID.ToString() == typeof(User)
                    .GetProperty("USER_ID")
                    .GetValue(dbObject).ToString());

                    user = (IQueryable<User>)dbObject.GetInstance<User>();
                    break;

                case "UserAdress":
                    var userAdress = _database.UsersAdresses
                    .Where(o => o.ADRESS_ID.ToString() == typeof(UsersAdress)
                    .GetProperty("ADRESS_ID")
                    .GetValue(dbObject).ToString());

                    userAdress = (IQueryable<UsersAdress>)dbObject.GetInstance<UsersAdress>();
                    break;

                case "State":
                    var state = _database.States
                    .Where(o => o.STATE_ID.ToString() == typeof(State)
                    .GetProperty("STATE_ID")
                    .GetValue(dbObject).ToString());

                    state = (IQueryable<State>)dbObject.GetInstance<State>();
                    break;

                default:
                    throw new InvalidOperationException();

            }
        }

        public IEnumerable<Item> GetItemsThatConfilctsWithDate(DateTime startDate, DateTime endDate)
        {
            IEnumerable<Item> results = _database.Reservations
                .Where(reservation => reservation.DATE_FROM < startDate && reservation.DATE_TO > startDate || reservation.DATE_FROM < endDate && reservation.DATE_TO > endDate)
                .Select(reservation => reservation.Item);
            //TODO dopisz warunek ktory zwroci itemy tylko o okreslonej nazwie

            return results;
        }
		
		public IEnumerable<User> FindUserByLogin(string userLogin)
		{
            IEnumerable<User> user = _database.Users
            .Where(u => u.LOGIN == userLogin);
            
            return user;
			//TODO sprawdz czy jest poprawnie i czemu tu musi byc lista
		}
    }
}
