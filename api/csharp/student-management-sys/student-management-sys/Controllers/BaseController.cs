using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using student_management_sys.Configs;

namespace student_management_sys.Controllers
{

    public class BaseController : ControllerBase
    {
        public readonly IMapper mapper;
        public ILogger<AccountController> logger;
        public readonly StudManSysDBContext context;

        public BaseController(ILogger<AccountController> logger, IMapper mapper, StudManSysDBContext context) {
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }
    }
}
