﻿using CoreTemplate.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreTemplate.Services
{
    public interface IPersonManager
    {
        public Task<object> GetAsync(ApplicationUser user, Expression<Func<Person, object>> expression);
        public Task<Person> GetPersonAsync(int id);
        public Task<Person> GetPersonAsync(ApplicationUser user);
        public Task<IEnumerable<Person>> GetPersonListAsync();

        public Task SetFirstNameAsync(ApplicationUser user, string firstName);
        public Task SetLastNameAsync(ApplicationUser user, string lastName);

    }
}
