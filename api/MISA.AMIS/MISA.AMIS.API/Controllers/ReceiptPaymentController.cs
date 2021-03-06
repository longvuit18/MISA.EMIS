using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.ApplicationCore.Constants;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using MISA.AMIS.ApplicationCore.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    /// <summary>
    /// Controller employee
    /// </summary>
    /// Created by: VLVU (29/7/2021)
    [Route("api/v1/receiptPayment")]
    [ApiController]
    public class ReceiptPaymentController : BaseController<ReceiptPayment>
    {
        #region Declared

        readonly IReceiptPaymentService _receiptPaymentService;
        #endregion

        #region Initialization
        public ReceiptPaymentController(IReceiptPaymentService receiptPaymentService) : base(receiptPaymentService)
        {
            _receiptPaymentService = receiptPaymentService;
        }
        #endregion

        #region Methods

        [HttpPost("filterPaging")]
        public async Task<IActionResult> GetFilterPaging(ReceiptPaymentFilter filter)
        {
            var result = await _receiptPaymentService.GetReceiptPaymentFilterPaging(filter);
            return Ok(result);
        }
        /// <summary>
        /// Lấy một mã nhân viên mới từ cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (20/8/2021)
        [HttpGet("newPaymentCode")]
        public async Task<IActionResult> GetNewPaymentCode()
        {
            var result = await _receiptPaymentService.GetNewReceiptPaymentCode();
            return Ok(result);
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromBody]Guid[] listId)
        {
            var result = await _receiptPaymentService.DeleteBatch(listId);
            return Ok(result);
        }

        [HttpPost("payment")]
        public async Task<IActionResult> InsertPayment([FromBody] PaymentRequest paymentRequest)
        {
            var result = await _receiptPaymentService.InsertPayment(paymentRequest.payment, paymentRequest.paymentDetails);
            return Ok(result);
        }

        [HttpGet("payment/{id}")]
        public async Task<IActionResult> GetPaymentById(string id)
        {
            // convert string to guid
            var isToGuid = Guid.TryParse(id, out Guid guid);
            if (!isToGuid)
            {
                var errorResult = new ServiceResult();
                errorResult.Code = MisaCode.Fail;
                errorResult.State = false;
                errorResult.MsgDev = Resources.IdInconrect;
                errorResult.MsgUser = Resources.IdInconrect;
                return Ok(errorResult);
            }

            var result = await _receiptPaymentService.GetPaymentById(guid);

            if (result.Data == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        #endregion

    }
}
