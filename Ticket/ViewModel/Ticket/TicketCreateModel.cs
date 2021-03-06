using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ticket.Enum;

namespace Ticket.ViewModels.Ticket
{
    public class TicketCreateModel
    {



        /// <summary>狀態:建立	已解決</summary>
        [Required]
        public TicketStatusEnum Status { get; set; }

        /// <summary>摘要</summary>
        [Required]
        public string Summary { get; set; }

        /// <summary>描述</summary>
        [Required]
        public string Desc { get; set; }

        /// <summary>嚴重性</summary>
        public int Serious { get; set; }

        /// <summary>優先級</summary>
        public int Priority { get; set; }



    }
}
