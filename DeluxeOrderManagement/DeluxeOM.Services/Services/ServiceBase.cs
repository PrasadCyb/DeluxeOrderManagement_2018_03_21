using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Services.Common;

namespace DeluxeOM.Services
{
    public abstract class ServiceBase
    {
        AccountAdapter _mapper = null;
        /// <summary>
        /// Return object of AccountAdapter
        /// </summary>
        protected AccountAdapter Mapper
        {
            get
            {
                return _mapper ?? new AccountAdapter();
            }
        }
    }
}
