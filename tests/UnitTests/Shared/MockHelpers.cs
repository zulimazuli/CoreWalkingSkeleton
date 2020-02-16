using CoreTemplate.ApplicationCore.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace UnitTests.Shared
{
    public static class MockHelpers
    {
        public static Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
