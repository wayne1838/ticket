using AutoMapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Ticket.Adapter.Ticket.Interface;
using Ticket.Db;
using Ticket.Enum;
using Ticket.Models.Ticket;
using Ticket.Service.Ticket.Interface;
using Ticket.ViewModels.Ticket;

namespace Ticket.Service.Ticket
{
    public class TicketService : ITicketService
    {

        private readonly ITicketAdapter _ticketAdapter;
        private readonly IMapper _mapper;

        public TicketService(ITicketAdapter TicketAdapter, IMapper mapper)
        {
            this._ticketAdapter = TicketAdapter;
            this._mapper = mapper;
        }

        public List<TicketDto> GetList(TicketSearchDto tacketDto)
        {

            return _mapper.Map<List<TicketDto>>(_ticketAdapter.GetList(tacketDto)); ;
        }

        public TicketDto Get(int id)
        {
            return _mapper.Map<TicketDto>(_ticketAdapter.Get(id));
        }

        public int Create(TicketDto model)
        {
            
            return _ticketAdapter.Create(_mapper.Map<TicketModel>(model));
        }

        public bool Update(TicketDto model)
        {
            return  _ticketAdapter.Update(_mapper.Map<TicketModel>(model));
        }

        public bool UpdateSolve(int id)
        {
            var data = _ticketAdapter.Get(id);
            data.Status = TicketStatusEnum.Solve;
            return _ticketAdapter.Update(data);
        }

        public bool Delete(int id)
        {
            return _ticketAdapter.Delete(id);
        }

    }
}
