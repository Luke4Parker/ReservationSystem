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
            var query = $"INSERT INTO Location (Name, City, State, Capacity, OpenTime, CloseTime, BrandId) " +
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
            var query = $"UPDATE Location SET Name = '{locationUpdates.Name}', City = '{locationUpdates.City}', State = '{locationUpdates.State}', " +
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

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var query = "SELECT * FROM Reservation";
            using (var connection = _context.CreateConnection())
            {
                var reservations = await connection.QueryAsync<Reservation>(query);

                return reservations.ToList();
            }
        }
    }

    
}
