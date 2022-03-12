using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ticket.Adapter.Ticket.Interface;
using Ticket.Models.Ticket;
using Ticket.ViewModels.Ticket;
using Ticket.Db;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;

namespace Ticket.Adapter.Ticket
{
    public class TicketAdapter : ITicketAdapter
    {

        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public TicketAdapter( MyContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public List<TicketModel> GetList(TicketSearchDto tacketDto)
        {

            var list = _context.Ticket.Where(w =>
                w.Type == tacketDto.Type &&
                (tacketDto.Status == null || w.Status == tacketDto.Status) &&
                w.Summary.Contains(tacketDto.Summary)
            ).ToList();

            return list;

            //using (var _context = new MyContext())
            //{
            //    var list = _context.Ticket.Where(w =>
            //    w.Type == tacketDto.Type &&
            //    (tacketDto.Status != null && w.Status == tacketDto.Status) &&
            //    w.Summary.Contains(tacketDto.Summary)
            //).ToList();

            //    return _mapper.Map<List<TicketModel>>(list);
            //}

        }

        public TicketModel Get(int id)
        {
            var data = _context.Ticket.FirstOrDefault(w => w.Id ==id);

            return data;
        }

        public int Create(TicketModel model)
        {
            _context.Ticket.Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        public bool Update(TicketModel model)
        {
            var data = Get(model.Id);
            data.Type = model.Type;
            data.Status = model.Status;
            data.Summary = model.Summary;
            data.Desc = model.Desc;
            data.Serious = model.Serious;
            data.Priority = model.Priority;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var data = Get(id);
            _context.Ticket.Remove(data);

            _context.SaveChanges();

            return true;
        }

    }
}
