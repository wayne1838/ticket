using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Enum
{
    /// <summary>
    /// 單據狀態
    /// </summary>
    public enum TicketStatusEnum
    {
        /// <summary>
        /// 無
        /// </summary>
        [Description("無")]
        None,
        /// <summary>
        /// 已解決
        /// </summary>
        [Description("已解決")]
        Solve,
    }
}
