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
        /// <summary>
        /// 失敗
        /// </summary>
        [Description("失敗")]
        Fail = 0,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1,
    }
}
