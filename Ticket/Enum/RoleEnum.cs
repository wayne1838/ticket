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
        /// <summary>
        /// QA
        /// </summary>
        [Description("QA")]
        QA,
        /// <summary>
        /// RD
        /// </summary>
        [Description("RD")]
        RD,
        /// <summary>
        /// PM
        /// </summary>
        [Description("PM")]
        PM,
        /// <summary>
        /// Admin
        /// </summary>
        [Description("Admin")]
        Admin,
    }


}
