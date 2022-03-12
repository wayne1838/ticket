using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ticket.Enum;

namespace Ticket.ViewModels.Ticket
{
    public class TicketResponseModel
    {
        /// <summary>ID</summary>
        public int Id { get; set; }

        /// <summary>類型: 錯誤	功能請求	測試用例 </summary>
        public TicketTypeEnum Type { get; set; }


        /// <summary>狀態:建立	已解決</summary>
        public TicketStatusEnum Status { get; set; }

        /// <summary>摘要</summary>
        public string Summary { get; set; }

        /// <summary>描述</summary>
        public string Desc { get; set; }

        /// <summary>嚴重性</summary>
        public int? Serious { get; set; }

        /// <summary>優先級</summary>
        public int? Priority { get; set; }



    }
}
