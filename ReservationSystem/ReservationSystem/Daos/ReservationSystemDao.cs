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
                var locations = await connection.ExecuteAsync(query);
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
