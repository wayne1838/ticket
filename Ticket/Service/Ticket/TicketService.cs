using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ticket.Adapter.Ticket.Interface;
using Ticket.Db;
using Ticket.Models.Ticket;
using Ticket.Service.Ticket.Interface;
using Ticket.ViewModels.Ticket;

namespace Ticket.Service.Ticket
{
    public class TicketService : ITicketService
    {

        private readonly ITicketAdapter _ticketAdapter;
        private readonly MyContext _context;

        public TicketService(ITicketAdapter TicketAdapter, MyContext context)
        {
            _ticketAdapter = TicketAdapter;
            _context = context;
        }

        public List<TicketModel> GetList(TicketSearchDto tacketDto)
        {

            return _ticketAdapter.GetList(tacketDto);
        }

        public TicketModel Get(int id)
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
