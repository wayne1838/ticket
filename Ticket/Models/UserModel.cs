using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ticket.Enum;

namespace Ticket.Models.Ticket
{
    public class UserModel
    {
        /// <summary>ID</summary>
        public int Id { get; set; }

        /// <summary>使用者名稱 </summary>
        public string UserName { get; set; }


        /// <summary>角色</summary>
        public RoleEnum Role { get; set; }



    }
}
