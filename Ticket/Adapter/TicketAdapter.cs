using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ticket.Adapter.Ticket.Interface;
using Ticket.Models.Ticket;
using Ticket.Adapter.Ticket.Interface;
using Ticket.ViewModels.Ticket;
using Ticket.Db;
using System.Linq;

namespace Ticket.Adapter.Ticket
{
    public class TicketAdapter : ITicketAdapter
    {

        private readonly MyContext _context;

        public TicketAdapter( MyContext context)
        {
            _context = context;
        }

        public List<TicketModel> GetList(TicketSearchDto tacketDto)
        {
            return _context.Ticket.Where(w => 
            w.Type == tacketDto.Type &&
            ( tacketDto.Status!=null && w.Status == tacketDto.Status) &&
            w.Summary.Contains(tacketDto.Summary)
            ).ToList();
            
        }

        public TicketModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Create(TicketRequestModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(TicketRequestModel model)
        {
            return true;
        }

        public bool Delete(int TicketId)
        {
            return true;
        }

    }
}
