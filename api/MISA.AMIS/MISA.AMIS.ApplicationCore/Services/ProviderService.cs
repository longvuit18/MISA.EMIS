﻿using MISA.AMIS.ApplicationCore.Constants;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Services
{
    public class ProviderService : BaseService<AccountObject>, IProviderService
    {
        #region Declared

        IProviderRepository _providerRepository;

        #endregion

        #region Initialization
        public ProviderService(IProviderRepository providerRepository) : base(providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ServiceResult> DeleteBatch(Guid[] listId)
        {
            var rowAffect =  await _providerRepository.DeleteBatch(listId);
            if(rowAffect < 1)
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

        public async Task<string> GetNewProviderCode()
        {
            return await _providerRepository.GetNewProviderCode();
        }

        public async Task<ServiceResult> GetProviderFilterPaging(ProviderFilter filter)
        {
            var data = await _providerRepository.GetProviderFilterPaging(filter);
            return new ServiceResult
            {
                Code = MisaCode.Success,
                Data = data,
                MsgUser = Properties.Resources.GetSuccessfully,
                State = true
            };
        }
        #endregion
    }
}
