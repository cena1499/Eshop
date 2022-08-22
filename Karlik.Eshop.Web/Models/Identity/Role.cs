using Microsoft.AspNetCore.Identity;
using System;

namespace Karlik.Eshop.Web.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }

        internal static object Admin()
        {
            throw new NotImplementedException();
        }

    }
}
