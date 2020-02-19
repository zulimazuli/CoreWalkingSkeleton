using System;
using System.Collections.Generic;
using System.Text;
using CoreTemplate.ApplicationCore.Models;
using Infrastructure.Data;

namespace CoreTemplate.Infrastructure.Data.Repositories
{
    public class PersonRepository : EfCoreRepository<Person, int>
    {
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
