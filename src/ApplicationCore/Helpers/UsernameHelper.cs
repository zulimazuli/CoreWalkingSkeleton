using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Extensions;
using CoreTemplate.ApplicationCore.Identity;
using Microsoft.AspNetCore.Identity;

namespace CoreTemplate.ApplicationCore.Helpers
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
            var generatedUsername= GenerateUsername(firstName, lastName);
            var newUsername = generatedUsername;
            var count = 0;
            while (await _userManager.FindByNameAsync(newUsername) != null)
            {
                newUsername = string.Concat(generatedUsername, (++count).ToString());
            }

            return newUsername;
        }

        private string GenerateUsername(string firstName, string lastName, 
            string prefix = null, string suffix = null)
        {
            var firstLetter = firstName.Substring(0, NumberOfFirstnameLetters);
            var followingLetters = lastName.Length >= MaxNumberOfLastnameLetters
                ? lastName.Substring(0, MaxNumberOfLastnameLetters)
                : lastName;

            var username = string.Concat(firstLetter, followingLetters)
                .ToLower()
                .RemoveDiacritics()
                .RemoveNonAlphabeticCharacters();

            if (!string.IsNullOrWhiteSpace(prefix))
            {
                username = string.Concat(prefix, username);
            }

            if (!string.IsNullOrWhiteSpace(suffix))
            {
                username = string.Concat(username, suffix);
            }

            return username;
        }
    }
}