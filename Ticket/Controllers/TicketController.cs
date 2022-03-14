using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        private readonly ILogger<TicketController> _logger;
        private readonly IMapper _mapper;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService,  IMapper mapper)
        {
            this._logger = logger;
            this._ticketService = ticketService;
            this._mapper = mapper;
        }

        /// <summary>
        /// 取得列表
        /// </summary>
        /// <param name="searchDto">條件</param>
        /// <returns></returns>
        [HttpPost("List")]
        public IActionResult GetList(TicketSearchDto searchDto)
        {
            if (searchDto is null) return BadRequest();
            var data = _ticketService.GetList(searchDto);
            return Ok(new ResponseModel
            {
                StatusCode = ResponseStatusEnum.Success,
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
                StatusCode = ResponseStatusEnum.Success,
                Data = data
            });
        }


        /// <summary>
        /// 新增錯誤
        /// </summary>
        /// <param name="requestModel">新增資料</param>
        /// <returns></returns>
        [Authorize(Roles = "QA")]
        [HttpPost("Error")]
        public IActionResult CreateError([FromBody] TicketCreateModel requestModel)
        {
            
            var dto = _mapper.Map<TicketDto>(requestModel);
            dto.Type = TicketTypeEnum.Error;

            return Create(dto);

        }

        /// <summary>
        /// 新增功能請求
        /// </summary>
        /// <param name="requestModel">新增資料</param>
        /// <returns></returns>
        [Authorize(Roles = "PM")]
        [HttpPost("FeatureRequest")]
        public IActionResult CreateFeatureRequest([FromBody] TicketCreateModel requestModel)
        {
            var dto = _mapper.Map<TicketDto>(requestModel);
            dto.Type = TicketTypeEnum.FeatureRequest;

            return Create(dto);

        }

        /// <summary>
        /// 新增測試用例
        /// </summary>
        /// <param name="requestModel">新增資料</param>
        /// <returns></returns>
        [Authorize(Roles = "QA")]
        [HttpPost("TestCase")]
        public IActionResult CreateTestCaseAsync([FromBody] TicketCreateModel requestModel)
        {
            var dto = _mapper.Map<TicketDto>(requestModel);
            dto.Type = TicketTypeEnum.TestCase;

            return Create(dto);

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto">新增資料</param>
        /// <returns></returns>
        private IActionResult Create([FromBody] TicketDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            dto.CreateUser = User.FindFirst("userId")==null? 0:Int32.Parse( User.FindFirst("userId")?.Value);

            var createId =  _ticketService.Create(dto);

            return Ok(new ResponseModel
            {
                StatusCode = createId > -1 ? ResponseStatusEnum.Success : ResponseStatusEnum.Fail,
                Data = createId
            });

        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="requestModel">內容</param>
        /// <returns></returns>
        [Authorize(Roles = "QA")]
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] TicketRequestModel requestModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var data = _ticketService.Get(id);
            //不該查無資料
            if (data is null) return BadRequest();

            //組合可以變更的資料
            data.Summary = requestModel.Summary;
            data.Desc = requestModel.Desc;
            data.UpdateUser = User.FindFirst("userId") == null ? 0 : Int32.Parse(User.FindFirst("userId")?.Value);

            var isSuccess = _ticketService.Update(data);

            return Ok(new ResponseModel
            {
                StatusCode = isSuccess ? ResponseStatusEnum.Success : ResponseStatusEnum.Fail,
                Data = null
            });

        }

        /// <summary>
        /// 更新狀態為解決
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(Roles = "RD")]
        [HttpPut("Status/{id}")]
        public IActionResult UpdateSolve([FromRoute] int id)
        {

            if (id>0)
            {
                return BadRequest();
            }
            //取得更新使用者ID
            var userId = User.FindFirst("userId") == null ? 0 : Int32.Parse(User.FindFirst("userId")?.Value);

            var isSuccess = _ticketService.UpdateSolve(id, userId);

            return Ok(new ResponseModel
            {
                StatusCode = isSuccess ? ResponseStatusEnum.Success : ResponseStatusEnum.Fail,
                Data = null
            });

        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "QA")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {

            var isSuccess = _ticketService.Delete(id);

            return Ok(new ResponseModel
            {
                StatusCode = isSuccess ? ResponseStatusEnum.Success : ResponseStatusEnum.Fail,
                Data = null
            });
        }

    }
}
