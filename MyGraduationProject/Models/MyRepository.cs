using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGraduationProject.Controllers;
using System.Collections;
using System.Data.Entity;
using MyGraduationProject.Models.Enums;

namespace MyGraduationProject.Models
{
    public class MyRepository
    {

        private DataClassesDataContext db = new DataClassesDataContext();

        public IEnumerable<User> GetAllUsers()
        {
            var users = db.Users;
            return users;
        }

        public User GetUserById(int id)
        {
            //FirstOrDefault() - dzieki temu zwroci tylko 1 rekord a jesli bedzie pusty to zwroci nulla
            var user = db.Users.Where(u => u.ADRESS_ID == id).FirstOrDefault();
            return user;
        }

        public IEnumerable<User> GetUserByLogin(string login)
        {
            var user = db.Users.Where(u => u.LOGIN == login);
            return user;
        }

        public User GetUserByLoginAndPass(string login, string password)
        {
            var user = db.Users.Where(u => u.LOGIN == login && u.PASSWORD == password).FirstOrDefault();
            return user;
        }

        public IEnumerable<Item> GetAllItems()
        {
            var items = db.Items;
            return items;
        }

        public IEnumerable<Item> GetAllStatusAvailableItems()
        {
            var items = db.Items.Where(i => i.STATE_ID == (int)StatesEnum.AVAILABLE);
            return items;
        }

        public Item GetItemById(int id)
        {
            var item = db.Items.Where(i => i.ITEM_ID == id).FirstOrDefault();
            return item;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            var item = db.Reservations;
            return item;
        }

        public Reservation GetReservationById(int id)
        {
            var reservation = db.Reservations.Where(r => r.RESERVATION_ID == id).FirstOrDefault();

            return reservation;
        }

        public IEnumerable<Item> GetListOfAvailableItems(DateTime startDate, DateTime endDate)
        {            
            //zapytanie zagniezdzone musi zwracac boola dla tego na koncu jest any(), any zwraca informacje czy wystepuje jakis element w liscie(jesli lista bedzie pusta to zwroci false w przeciwnym wypadku frue)
            // pobierze liste itemow dostepnych w chwili wywolania widoku
            var itemsOfOutRange = db.Items.Where(i => i.Reservations.Where(reservation => reservation.DATE_FROM > startDate && reservation.DATE_FROM > endDate || reservation.DATE_TO < startDate && reservation.DATE_TO < endDate || reservation.STATUS_ID == (int)ReservationStatusesEnum.CANCELLED).Any() && i.STATE_ID == (int)(StatesEnum.AVAILABLE));
            var itemsWithoutReservation = db.Items.Where(i => !i.Reservations.Any() && i.STATE_ID == (int)(StatesEnum.AVAILABLE));
            var listOfAvailableItems = new List<Item>(); // Finalna lista dostepnych itemow
            listOfAvailableItems.AddRange(itemsOfOutRange);
            listOfAvailableItems.AddRange(itemsWithoutReservation);

            return listOfAvailableItems;
        }

        public bool isItemAvailable (int id, DateTime startDate, DateTime endDate)
        {
            //dostępnosc produktu podczas tworzenia rezerwacji
            //isAvailable powinno byc false dla 2, dla 3,6 true
            var itemToBook = db.Items.Where(i => i.ITEM_ID == id && i.STATE_ID == (int)(StatesEnum.AVAILABLE)).FirstOrDefault();
            var isOutOfDateRange = itemToBook.Reservations.Where(reservation => reservation.DATE_FROM > startDate && reservation.DATE_FROM > endDate || reservation.DATE_TO < startDate && reservation.DATE_TO < endDate || reservation.STATUS_ID == (int)ReservationStatusesEnum.CANCELLED).Any();
            var haveNoReservation = !itemToBook.Reservations.Any();
            var isAvailable = isOutOfDateRange || haveNoReservation; // czy dany item jest dostepny
            return isAvailable;
        }

        public List<Role> GetAllRoles()
        {
            var roles = db.Roles;
            return roles.ToList();
        }

        public List<State> GetAllStatuses()
        {
            var statuses = db.States;
            return statuses.ToList();
        }

        public List<ReservationStatuse> GetAllReservationStatuses()
        {
            var reservationStatuses = db.ReservationStatuses;
            return reservationStatuses.ToList();
        }
        
        public IEnumerable<Item> GetAvailableItems()
        {
            var items = db.Items.Where(i => i.STATE_ID == (int)(StatesEnum.AVAILABLE));
            return items;
        }
    }
}