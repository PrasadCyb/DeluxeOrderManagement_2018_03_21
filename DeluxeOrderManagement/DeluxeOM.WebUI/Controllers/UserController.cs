using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeluxeOM.Services;
using DeluxeOM.Models.Account;
using DeluxeOM.Models;

namespace DeluxeOM.WebUI.Controllers
{
    [DeluxeOMAuthorize(Roles = "SystemAdmin")]
    public class UserController : BaseController
    {
       

    }
}