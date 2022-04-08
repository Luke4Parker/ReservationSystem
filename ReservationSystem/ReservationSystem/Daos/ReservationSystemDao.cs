using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace ReservationSystem.Daos
{
    public class ReservationSystemDao
    {
        private readonly DapperContext _context; 
        public ReservationSystemDao(DapperContext context)
        {
            _context = context;
        }
        
        //************************************************************
        //Begin Location Sql Requests
        public async Task CreateLocation(Location newLocation)
        {
            var query = $"INSERT INTO Location ([Name], City, [State], Capacity, OpenTime, CloseTime, BrandId) " +
                $"VALUES ('{newLocation.Name}', '{newLocation.City}', '{newLocation.State}', {newLocation.Capacity}," +
                $" '{newLocation.OpenTime}', '{newLocation.CloseTime}', {newLocation.BrandId})";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            var query = "SELECT * FROM Location";
            using (var connection = _context.CreateConnection())
            {
                var locations = await connection.QueryAsync<Location>(query);

                return locations.ToList();
            }
        }

        public async Task<Location> GetLocationById(int id)
        {
            var query = $"SELECT * FROM Location WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                var location = await connection.QueryFirstOrDefaultAsync<Location>(query);

                return location;
            }
        }

        public async Task DeleteLocation(int id)
        {
            var query = $"DELETE FROM Location WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);                
            }
        }

        public async Task UpdateLocation(Location locationUpdates, int id)
        {
            var query = $"UPDATE Location SET [Name] = '{locationUpdates.Name}', City = '{locationUpdates.City}', [State] = '{locationUpdates.State}', " +
                $"Capacity = {locationUpdates.Capacity}, OpenTime = '{locationUpdates.OpenTime}', " +
                $"CloseTime = '{locationUpdates.CloseTime}', BrandId = {locationUpdates.BrandId} WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        //***************************************************
        //Begin Customer SQL requests

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var query = "SELECT * FROM Customer";
            using (var connection = _context.CreateConnection())
            {
                var customers = await connection.QueryAsync<Customer>(query);

                return customers.ToList();
            }
        }

        public async Task CreateCustomer(Customer newCustomer)
        {
            var query = $"INSERT INTO Customer (FirstName, LastName, Phone, Email) " +
                $"VALUES ('{newCustomer.FirstName}', '{newCustomer.LastName}', '{newCustomer.Phone}', '{newCustomer.Email}')";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var query = $"SELECT * FROM Customer WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                var customer = await connection.QueryFirstOrDefaultAsync<Customer>(query);

                return customer;
            }
        }

        public async Task DeleteCustomer(int id)
        {
            var query = $"DELETE FROM Customer WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateCustomer(Customer customerUpdates, int id)
        {
            var query = $"UPDATE Customer SET FirstName = '{customerUpdates.FirstName}', LastName = '{customerUpdates.LastName}', Email = '{customerUpdates.Email}', " +
                $"Phone = '{customerUpdates.Phone}' WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        //End Customer SQL Requests
        //******************************************************
        //Begin Reservation SQL Requests

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var query = "SELECT * FROM Reservation";
            using (var connection = _context.CreateConnection())
            {
                var reservations = await connection.QueryAsync<Reservation>(query);

                return reservations.ToList();
            }
        }

        public async Task CreateReservation(Reservation newReservation)
        {
            var query = $"INSERT INTO Reservation ([Length], PartySize, ReservationTime, CustomerId, LocationId) " +
                $"VALUES ('{newReservation.Length}', '{newReservation.PartySize}', '{newReservation.ReservationTime}', {newReservation.CustomerId}, {newReservation.LocationId})";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            var query = $"SELECT * FROM Reservation WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                var reservation = await connection.QueryFirstOrDefaultAsync<Reservation>(query);

                return reservation;
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservationByCustomerId(int id)
        {
            var query = $"SELECT * FROM Reservation WHERE CustomerId = {id}";
            using (var connection = _context.CreateConnection())
            {
                var reservation = await connection.QueryAsync<Reservation>(query);

                return reservation.ToList();
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservationByLocationId(int id)
        {
            var query = $"SELECT * FROM Reservation WHERE LocationId = {id}";
            using (var connection = _context.CreateConnection())
            {
                var reservation = await connection.QueryAsync<Reservation>(query);

                return reservation.ToList();
            }
        }

        public async Task DeleteReservation(int id)
        {
            var query = $"DELETE FROM Reservation WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateReservation(Reservation reservationUpdates, int id)
        {
            var query = $"UPDATE Reservation SET [Length] = {reservationUpdates.Length}, PartySize = {reservationUpdates.PartySize}, ReservationTime = '{reservationUpdates.ReservationTime}', " +
                $"CustomerId = {reservationUpdates.CustomerId}, LocationId = {reservationUpdates.LocationId} WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
    }

    
}
