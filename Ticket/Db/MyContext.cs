using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ticket.Models.Ticket;

namespace Ticket.Db
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<TicketModel> Ticket { get; set; }
    }
}
