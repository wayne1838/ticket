using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Enum
{
    /// <summary>
    /// 角色
    /// </summary>
    public enum RoleEnum
    {
        [Description("QA")]
        QA,
        [Description("RD")]
        RD,
        [Description("PM")]
        PM,
        [Description("Admin")]
        Admin,
    }


}
