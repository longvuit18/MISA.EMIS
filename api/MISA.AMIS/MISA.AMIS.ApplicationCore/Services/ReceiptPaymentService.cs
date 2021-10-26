using MISA.AMIS.ApplicationCore.Constants;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Services
{
    public class ReceiptPaymentService : BaseService<ReceiptPayment>, IReceiptPaymentService
    {
        #region Declared

        IReceiptPaymentRepository _receiptPaymentRepository;

        #endregion

        #region Initialization
        public ReceiptPaymentService(IReceiptPaymentRepository receiptPaymentRepository) : base(receiptPaymentRepository)
        {
            _receiptPaymentRepository = receiptPaymentRepository;
        }
        #endregion

        public async Task<ServiceResult> DeleteBatch(Guid[] listId)
        {
            var rowAffect = await _receiptPaymentRepository.DeleteBatch(listId);
            if (rowAffect < 1)
            {
                return new ServiceResult
                {
                    Code = MisaCode.Success,
                    Data = rowAffect,
                    MsgUser = Properties.Resources.NothingUpdate,
                    State = false
                };
            }
            return new ServiceResult
            {
                Code = MisaCode.Success,
                Data = rowAffect,
                MsgUser = Properties.Resources.GetSuccessfully,
                State = true
            };
        }

        public async Task<string> GetNewReceiptPaymentCode()
        {
            return await _receiptPaymentRepository.GetNewReceiptPaymentCode();
        }

        public async Task<ServiceResult> GetPaymentById(Guid paymentId)
        {
            var data = await _receiptPaymentRepository.GetPaymentById(paymentId);
            return new ServiceResult
            {
                Code = MisaCode.Success,
                Data = data,
                MsgUser = Properties.Resources.GetSuccessfully,
                State = true
            };
        }

        public async Task<ServiceResult> GetReceiptPaymentFilterPaging(ReceiptPaymentFilter filter)
        {
            var data = await _receiptPaymentRepository.GetReceiptPaymentFilterPaging(filter);
            return new ServiceResult
            {
                Code = MisaCode.Success,
                Data = data,
                MsgUser = Properties.Resources.GetSuccessfully,
                State = true
            };
        }

        public async Task<ServiceResult> InsertPayment(ReceiptPayment receiptPayment, ReceiptPaymentDetail[] receiptPaymentDetails)
        {
            // validate entity
            var isValid = await Validate(receiptPayment);
            var serviceResult = new ServiceResult();
            if (!isValid)
            {
                serviceResult.Code = MisaCode.Fail;
                serviceResult.State = false;
                serviceResult.MsgUser = errorValidate;
                serviceResult.MsgDev = errorValidate;
                return serviceResult;
            }
            var rowCount = await _receiptPaymentRepository.InsertPayment(receiptPayment, receiptPaymentDetails);
            if (rowCount > 0)
            {
                return new ServiceResult
                {
                    Code = MisaCode.Success,
                    Data = rowCount,
                    MsgUser = Properties.Resources.AddedSuccessfully,
                    State = true
                };
            }

            // khi rowCount = 0 TODO
            serviceResult.Code = MisaCode.Success;
            serviceResult.State = false;
            serviceResult.Data = rowCount;
            serviceResult.MsgUser = Properties.Resources.NothingUpdate;
            return serviceResult;
        }
    }
}
