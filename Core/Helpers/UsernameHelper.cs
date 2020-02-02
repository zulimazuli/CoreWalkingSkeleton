using CoreTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Helpers
{
    public class UsernameHelper : IUsernameHelper
    {
        private const int NumberOfFirstnameLetters = 1;
        private const int NumberOfLastnameLetters = 7;
        public string GenerateUsername(string firstName, string lastName)
        {

            string firstLetter = firstName.Trim().Substring(0, NumberOfFirstnameLetters);
            string followingLetters = lastName.Length >= NumberOfLastnameLetters
                ? lastName.Trim().Substring(0, NumberOfLastnameLetters)
                : lastName.Trim();

            return string.Concat(firstLetter, followingLetters).ToLower();
           
        }
    }
}
