using System;
using System.Collections.Generic;
using System.Text;
using CoreTemplate.ApplicationCore.Models;
using Infrastructure.Data;

namespace CoreTemplate.Infrastructure.Data.Repositories
{
    public class ItemRepository : EfCoreRepository<Item, int>
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
