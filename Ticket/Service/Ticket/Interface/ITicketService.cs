
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Models.Ticket;
using Ticket.ViewModels.Ticket;

namespace Ticket.Service.Ticket.Interface
{
    public interface ITicketService
    {

        /// <summary>
        /// 取得或搜尋所有內容
        /// </summary>
        /// <returns></returns>
        List<TicketDto> GetList(TicketSearchDto tacketDto);

        /// <summary>
        ///取得資料 by ID
        /// </summary>
        /// <returns></returns>
        TicketDto Get(int id);


        /// <summary>
        /// 新增合約管理
        /// </summary>
        /// <returns></returns>
        int Create(TicketDto model);


        /// <summary>
        /// 更新合約頁面資料
        /// </summary>
        /// <returns></returns>
        bool Update(TicketDto model);

        /// <summary>
        /// 更新狀態為解決
        /// </summary>
        bool UpdateSolve(int id, int updateUserId);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <returns></returns>
        bool Delete(int id);
    }
}
