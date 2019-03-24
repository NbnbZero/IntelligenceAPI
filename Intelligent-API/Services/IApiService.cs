using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelligent.API.Services
{
    interface IApiService
    {
        Task<IList<string>> GetValues();
    }
}
