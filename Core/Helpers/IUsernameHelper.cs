using System.Threading.Tasks;

namespace CoreTemplate.Helpers
{
    public interface IUsernameHelper
    {
        Task<string> GenerateUniqueUsernameAsync(string firstName, string lastName);
    }
}
