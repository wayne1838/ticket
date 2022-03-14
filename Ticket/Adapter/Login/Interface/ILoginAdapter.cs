
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Models.Ticket;
using Ticket.Models.User;
using Ticket.ViewModels.Ticket;

namespace Ticket.Adapter.Login.Interface
{
    public interface ILoginAdapter
    {

        /// <summary>
        ///取得資料 
        /// </summary>
        /// <returns></returns>
        UserModel Get(string userName);


    }
}
