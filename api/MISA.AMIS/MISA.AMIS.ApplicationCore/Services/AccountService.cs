using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Services
{
    public class AccountService: BaseService<Account>
    {
        public AccountService(IBaseRepository<Account> baseRepository): base(baseRepository) {
        }
    }
}
