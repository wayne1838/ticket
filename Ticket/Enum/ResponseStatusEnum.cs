using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Enum
{
    /// <summary>
    /// api回傳狀態
    /// </summary>
    public enum ResponseStatusEnum
    {
        [Description("失敗")]
        Fail = 0,

        [Description("成功")]
        Success = 1,
    }
}
