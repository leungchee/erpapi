using ERPAPI.Dtos;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.Controllers
{
    [ApiController]
    [Route("openApi/thirdApi/v3")]
    public class ProportioningSyncController : ControllerBase
    {
        /// <summary>
        /// ERP同步配合比到智选
        /// </summary>
        [HttpPost("acceptProportioning")]
        [Authorize]
        [ProducesResponseType(typeof(ProportioningSyncResponse), 200)]
        [ProducesResponseType(typeof(ProportioningSyncResponse), 400)]
        public IActionResult AcceptProportioning([FromBody] List<ProportioningSyncDto> request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ProportioningSyncResponse
                {
                    Code = "400",
                    Msg = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)),
                    Data = "false"
                });
            }

            // TODO: 业务处理逻辑

            return Ok(new ProportioningSyncResponse
            {
                Code = "200",
                Msg = "同步成功",
                Data = "true"
            });
        }
    }
} 