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
using Ticket.Models.User;
using Ticket.Adapter.Login.Interface;

namespace Ticket.Adapter.Login
{
    public class LoginAdapter : ILoginAdapter
    {

        private readonly MyContext _context;

        public LoginAdapter( MyContext context, IMapper mapper)
        {
            this._context = context;
        }


        public UserModel Get(string userName)
        {
            var data = _context.User.FirstOrDefault(f => f.UserName == userName);

            return data;
        }

    }
}
