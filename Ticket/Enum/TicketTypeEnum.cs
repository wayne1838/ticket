using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Enum
{
    /// <summary>
    /// 建單類型
    /// </summary>
    public enum TicketTypeEnum
    {
        /// <summary>
        /// 錯誤
        /// </summary>
        [Description("錯誤")]
        Error,
        /// <summary>
        /// 功能請求
        /// </summary>
        [Description("功能請求")]
        FunctionRequest,
        /// <summary>
        /// 測試用例
        /// </summary>
        [Description("測試用例")]
        TestCase,
    }
}
