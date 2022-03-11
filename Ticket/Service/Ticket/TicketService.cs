using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ticket.Adapter.Ticket.Interface;
using Ticket.Models.Ticket;
using Ticket.Service.Ticket.Interface;

namespace Ticket.Service.Ticket
{
    public class TicketService : ITicketService
    {

        private readonly ITicketAdapter _ticketAdapter;

        public TicketService(ITicketAdapter TicketAdapter)
        {
            _ticketAdapter = TicketAdapter;
        }

        public IEnumerable<TicketRequestModel> GetList(TicketSearchDto tacketDto)
        {
            return _ticketAdapter.GetList(tacketDto);
        }

        public TicketRequestModel Get(int id)
        {
            return _ticketAdapter.Get(id);
        }

        public int Create(TicketRequestModel model)
        {
            return _ticketAdapter.Create(model);
        }

        public bool Update(TicketRequestModel model)
        {
            return _ticketAdapter.Update(model);
        }

        public bool Delete(int id)
        {
            return _ticketAdapter.Delete(id);
        }

    }
}
