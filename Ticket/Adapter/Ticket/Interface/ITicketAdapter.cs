
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Models.Ticket;
using Ticket.ViewModels.Ticket;

namespace Ticket.Adapter.Ticket.Interface
{
    public interface ITicketAdapter
    {

        /// <summary>
        /// 取得或搜尋所有內容
        /// </summary>
        /// <returns></returns>
        List<TicketModel> GetList(TicketSearchDto tacketDto);

        /// <summary>
        ///取得資料 by ID
        /// </summary>
        /// <returns></returns>
        TicketModel Get(int id);


        /// <summary>
        /// 新增合約管理
        /// </summary>
        /// <returns></returns>
        int Create(TicketModel model);


        /// <summary>
        /// 更新合約頁面資料
        /// </summary>
        /// <returns></returns>
        bool Update(TicketModel model);



        /// <summary>
        /// 刪除
        /// </summary>
        /// <returns></returns>
        bool Delete(int wineId);
    }
}
