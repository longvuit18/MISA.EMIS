using Microsoft.Extensions.Configuration;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure
{
    public class AccountRepository: BaseRepository<Account>
    {
        public AccountRepository(IConfiguration configuration) : base(configuration) { }
    }
}
