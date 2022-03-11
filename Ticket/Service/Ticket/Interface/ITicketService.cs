
using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Models.Ticket;

namespace Ticket.Service.Ticket.Interface
{
    public interface ITicketService
    {

        /// <summary>
        /// 取得或搜尋所有內容
        /// </summary>
        /// <returns></returns>
       IEnumerable<TicketRequestModel> GetList(TicketSearchDto tacketDto);

        /// <summary>
        ///取得資料 by ID
        /// </summary>
        /// <returns></returns>
        TicketRequestModel Get(int id);


        /// <summary>
        /// 新增合約管理
        /// </summary>
        /// <returns></returns>
        int Create(TicketRequestModel model);


        /// <summary>
        /// 更新合約頁面資料
        /// </summary>
        /// <returns></returns>
        bool Update(TicketRequestModel model);



        /// <summary>
        /// 刪除
        /// </summary>
        /// <returns></returns>
        bool Delete(int id);
    }
}
