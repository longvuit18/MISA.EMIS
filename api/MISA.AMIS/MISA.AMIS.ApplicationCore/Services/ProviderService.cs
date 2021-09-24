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
    public class ProviderService : BaseService<Provider>, IProviderService
    {
        #region Declared

        IProviderRepository _providerRepository;

        #endregion

        #region Initialization
        public ProviderService(IProviderRepository providerRepository) : base(providerRepository)
        {
            _providerRepository = providerRepository;
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
