using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountController : BaseController<Account>
    {
        public AccountController(IBaseService<Account> baseService): base(baseService)
        {

        }
    }
}
