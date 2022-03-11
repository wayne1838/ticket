using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ticket.Adapter.Ticket.Interface;
using Ticket.Models.Ticket;
using Ticket.Service.Ticket.Interface;

namespace Ticket.Service.Ticket
{
    public class TicketAdapter : ITicketAdapter
    {

        private readonly ITicketAdapter _ticketAdapter;

        public IEnumerable<TicketRequestModel> GetList(TicketSearchDto tacketDto)
        {
            throw new System.NotImplementedException();
        }

        public TicketRequestModel Get(int id)
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
