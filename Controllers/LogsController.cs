using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkDayLog.Core.Extensions;
using WorkDayLog.Domain.Logs.Services;
using WorkDayLog.Requests;

namespace WorkDayLog.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]")]    
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
            => _logService = logService;

        [HttpPost]
        public ActionResult<object> Post([FromBody] LogSubmit newLog)
        {
            var userId = this.GetAuthenticatedUserId();

            var response = _logService.Add(userId, newLog);

            return response.Success
                ? StatusCode(StatusCodes.Status201Created, response)
                : BadRequest(response);
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(Guid id, [FromBody] LogSubmit edit)
        {
            var response = _logService.Update(id, edit);

            return response.Success
                ? StatusCode(StatusCodes.Status200OK, response)
                : BadRequest(response);
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
            => _logService.GetAllByUser(this.GetAuthenticatedUserId()).ToList();

        [HttpGet("summary/{showDetails=false}")]
        public ActionResult<object> Summary(bool showDetails)
            => _logService.GetSummaryByUser(this.GetAuthenticatedUserId(), showDetails);
    }
}