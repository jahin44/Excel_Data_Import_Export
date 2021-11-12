using DataImporter.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.Seeds
{
    public static class DataSeed
    {
        public static Role[] Roles
        {
            get
            {
                return new Role[]
                {
                    new Role { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                    new Role { Id = Guid.NewGuid(), Name = "CommonUser", NormalizedName = "COMMONUSER", ConcurrencyStamp = Guid.NewGuid().ToString() }
                };
            }
        }
    }
}
