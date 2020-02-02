using CoreTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Helpers
{
    public interface IUsernameHelper
    {
        string GenerateUsername(string firstName, string lastName);
    }
}
