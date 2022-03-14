
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Models.Ticket;
using Ticket.Models.User;
using Ticket.ViewModels.Ticket;

namespace Ticket.Service.Login.Interface
{
    public interface ILoginService
    {
        //取得使用者資訊
        UserModel Get(string userName);

        /// <summary>
        /// 取得JWT token資訊
        /// </summary>
        /// <returns></returns>
        string GetToken(UserModel user, int expireMinutes=30);
    }
}
