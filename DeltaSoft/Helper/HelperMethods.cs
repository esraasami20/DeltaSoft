﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DeltaSoft.Helper
{
    public class HelperMethods
    {
        public static string GetAuthnticatedUserId(IIdentity UserIdentity)
        {

            var claimsIdentity = UserIdentity as ClaimsIdentity;
            string userId = claimsIdentity.Claims.ToList()[3].Value;
            return userId;

        }
    }
}
