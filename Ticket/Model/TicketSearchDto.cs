﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ticket.Enum;

namespace Ticket.Models.Ticket
{
    public class TicketSearchDto
    {

        /// <summary>類型: 錯誤	功能請求	測試用例 </summary>
        public TicketType Type { get; set; }

        /// <summary>狀態:建立	已解決</summary>
        public TicketStatus Status { get; set; }

        /// <summary>摘要</summary>
        public string Summary { get; set; }



    }
}
