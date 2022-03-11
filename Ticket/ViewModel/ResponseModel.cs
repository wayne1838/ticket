using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ticket.Enum;

namespace Ticket.ViewModels
{
    public class ResponseModel
    {
        /// <summary>回傳前端的狀態</summary>
        public ResponseStatus StatusCode { get; set; }

        /// <summary>描述</summary>
        public object? Data { get; set; }


    }
}
