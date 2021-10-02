using DeltaSoft.DTO;
using DeltaSoft.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeltaSoft.Services.Interfaces
{
    public interface IAuthentication
    {
        public Task<ResponseAuth> RegisterEmployeeAsync(RegisterModel model);
        public Task<ResponseAuth> RegisterAdminAsync(RegisterModel model);
        public Task<ResponseAuth> LoginAsync(LoginModel model);
    }
}
