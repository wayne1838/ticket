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
        [Description("無")]
        None,
        [Description("錯誤")]
        Error,
        [Description("功能請求")]
        FunctionRequest,
        [Description("測試用例")]
        TestCase,
    }
}
