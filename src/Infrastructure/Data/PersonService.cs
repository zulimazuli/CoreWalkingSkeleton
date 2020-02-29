using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Infrastructure.Data
{
    public class PersonService : IPersonService
    {
        
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAsync(ApplicationUser user, Expression<Func<Person, object>> expression)
        {
            var person = await GetPersonAsync(user);
            if (person == null)
            {
                return null;
            }

            var memberExpression = expression.Body as MemberExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;

            return propertyInfo.GetValue(person);
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task<Person> GetPersonAsync(ApplicationUser user)
        {
            return await _context.Person.FindAsync(user.PersonId);
        }

        public async Task<IEnumerable<Person>> GetPersonListAsync()
        {
            return await _context.Person.ToListAsync();
        }
        private async Task<Person> GetOrCreatePersonForUserAsync(ApplicationUser user)
        {
            var person = await GetPersonAsync(user);
            if (person == null)
            {
                user.Person = new Person();
                person = user.Person;
            }

            return person;
        }

        public async Task SetFirstNameAsync(ApplicationUser user, string firstName)
        {
            if (user != null)
            {
                Person person = await GetOrCreatePersonForUserAsync(user);
                person.FirstName = firstName;

                var x = _context.Person.Update(person);
                await _context.SaveChangesAsync();
            }
        }        

        public async Task SetLastNameAsync(ApplicationUser user, string lastName)
        {
            if (user != null)
            {
                Person person = await GetOrCreatePersonForUserAsync(user);
                person.LastName = lastName;

                var x = _context.Person.Update(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}
