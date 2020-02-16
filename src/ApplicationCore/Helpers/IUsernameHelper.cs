using System.Threading.Tasks;

namespace CoreTemplate.ApplicationCore.Helpers
{
    public interface IUsernameHelper
    {
        Task<string> GenerateUniqueUsernameAsync(string firstName, string lastName);
    }
}
