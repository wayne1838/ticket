﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Db;
using Ticket.Enum;
using Ticket.Models.Ticket;
using Ticket.Service.Ticket.Interface;
using Ticket.ViewModels;
using Ticket.ViewModels.Ticket;

namespace Ticket.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        private readonly ILogger<TicketController> _logger;
        private readonly MyContext _context;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService, MyContext context)
        {
            this._logger = logger;
            this._ticketService = ticketService;
            _context = context;
        }


        [HttpGet("hello")]
        public IActionResult Hello()
        {
            //https://localhost:44368/api/ticket/hello
            var testData = _context.Ticket.ToList();

            List<TicketModel> testData1 = _context.Ticket.Where(w=>w.Id==1).ToList();
            return Ok("Hello");
        }

        /// <summary>
        /// 取得列表
        /// </summary>
        /// <param name="searchDto">條件</param>
        /// <returns></returns>
        [HttpPost("List")]
        [HttpGet]
        public IActionResult GetList(TicketSearchDto searchDto)
        {
            if (searchDto is null) return BadRequest();
            var data = _ticketService.GetList(searchDto);
            return Ok(new ResponseModel
            {
                StatusCode = ResponseStatus.Success,
                Data = data
            });
        }

        /// <summary>
        /// 取得基本資料
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns >
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var data = _ticketService.Get(id);
            //不該查無資料
            if (data is null) return BadRequest();

            return Ok(new ResponseModel
            {
                StatusCode = ResponseStatus.Success,
                Data = data
            });
        }


        /// <summary>
        /// 新增錯誤
        /// </summary>
        /// <param name="requestModel">新增資料</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateError([FromBody] TicketCreateReq requestModel)
        {
            requestModel.Type = TicketType.Error;

            return Create(requestModel);

        }

        /// <summary>
        /// 新增功能請求
        /// </summary>
        /// <param name="requestModel">新增資料</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateFunctionRequest([FromBody] TicketCreateReq requestModel)
        {
            requestModel.Type = TicketType.FunctionRequest;

            return Create(requestModel);

        }

        /// <summary>
        /// 新增測試用例
        /// </summary>
        /// <param name="requestModel">新增資料</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTestCase([FromBody] TicketCreateReq requestModel)
        {
            requestModel.Type = TicketType.TestCase;

            return Create(requestModel);

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="requestModel">新增資料</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] TicketCreateReq requestModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createId = 0;// _ticketService.Create(requestModel);

            return Ok(new ResponseModel
            {
                StatusCode = createId > -1 ? ResponseStatus.Success : ResponseStatus.Fail,
                Data = createId
            });

        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="requestModel">內容</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] TicketRequestModel requestModel)
        {

            var isSuccess = _ticketService.Update(requestModel);

            return Ok(new ResponseModel
            {
                StatusCode = isSuccess ? ResponseStatus.Success : ResponseStatus.Fail,
                Data = null
            });

        }

        /// <summary>
        /// 更新狀態為解決
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="requestModel">內容</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateStatus([FromRoute] int id, [FromBody] TicketRequestModel requestModel)
        {

            var isSuccess = _ticketService.Update(requestModel);

            return Ok(new ResponseModel
            {
                StatusCode = isSuccess ? ResponseStatus.Success : ResponseStatus.Fail,
                Data = null
            });

        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {

            var isSuccess = _ticketService.Delete(id);

            return Ok(new ResponseModel
            {
                StatusCode = isSuccess ? ResponseStatus.Success : ResponseStatus.Fail,
                Data = null
            });
        }

    }
}
