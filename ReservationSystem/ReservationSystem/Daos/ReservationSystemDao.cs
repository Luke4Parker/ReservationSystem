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
        

    }

    
}
