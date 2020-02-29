using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Identity;

namespace CoreTemplate.Infrastructure.Data
{
    public interface IPersonService
    {
        //TODO: extract to repo
        Task<object> GetAsync(ApplicationUser user, Expression<Func<Person, object>> expression);
        Task<Person> GetPersonAsync(int id);
        Task<Person> GetPersonAsync(ApplicationUser user);
        Task<IEnumerable<Person>> GetPersonListAsync();

        //todo: service
        Task SetFirstNameAsync(ApplicationUser user, string firstName);
        Task SetLastNameAsync(ApplicationUser user, string lastName);

    }
}
