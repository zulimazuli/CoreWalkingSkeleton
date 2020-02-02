using System.Threading.Tasks;
using CoreTemplate.Extensions;
using CoreTemplate.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreTemplate.Helpers
{
    public class UsernameHelper : IUsernameHelper
    {
        private const int NumberOfFirstnameLetters = 1;
        private const int MaxNumberOfLastnameLetters = 50;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsernameHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateUniqueUsernameAsync(string firstName, string lastName)
        {
            var i = 0;
            string username;
            do
            {
                var suffix = i == 0 ? null : i.ToString();
                username = GenerateUsername(firstName, lastName, suffix);
                i++;
            } while (await _userManager.FindByNameAsync(username) != null);

            return username;
        }

        private string GenerateUsername(string firstName, string lastName, string suffix = null)
        {
            var firstLetter = firstName.Substring(0, NumberOfFirstnameLetters);
            var followingLetters = lastName.Length >= MaxNumberOfLastnameLetters
                ? lastName.Substring(0, MaxNumberOfLastnameLetters)
                : lastName;

            var username = string.Concat(firstLetter, followingLetters)
                .ToLower()
                .RemoveDiacritics()
                .RemoveNonAlphabeticCharacters();

            if (!string.IsNullOrWhiteSpace(suffix)) username = string.Concat(username, suffix);

            return username;
        }
    }
}