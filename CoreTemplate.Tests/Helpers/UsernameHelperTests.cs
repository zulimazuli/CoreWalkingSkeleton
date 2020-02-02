using CoreTemplate.Models;
using CoreTemplate.Tests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using CoreTemplate.Helpers;

namespace CoreTemplate.Tests.Helpers
{
    public class UsernameHelperTests
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private IUsernameHelper _sut;

        [SetUp]
        public void Setup()
        {
            _userManagerMock = MockHelpers.GetMockUserManager();
        }

        [TestCase("Jan", "Kowalski", "jkowalski")]
        //[TestCase("Andrzej", "Bżęściółką", "abzesciolka")] //TODO: doesnt work with Ł letter
        [TestCase("Anna", "B", "ab")]
        [TestCase("rYsZARD", "bieLECKI-MatyJA", "rbieleckimatyja")]
        [TestCase("Jezus Gonzalez Iglesias", "Don Kichot", "jdonkichot")]
        public async Task GenerateUniqueUsernameAsync_returnsValidUsername(string  firstname, string lastname, string expected)
        {
            // Arrange
            string result = null;
            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            
            // Act
            _sut = new UsernameHelper(_userManagerMock.Object);
            result = await _sut.GenerateUniqueUsernameAsync(firstname, lastname);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public async Task GenerateUniqueUsernameAsync_UsernameExists_returnValidUsernameWithSuffix()
        {
            // Arrange
            string result = null;
            var firstname = "John";
            var lastname = "Smith";

            var existingUsernames = new[] { "jsmith", "jsmith1" };

            var expectedResult = "jsmith2";

            _userManagerMock.Setup(x => x.FindByNameAsync(existingUsernames[0])).ReturnsAsync(new ApplicationUser { UserName = existingUsernames[0] });
            _userManagerMock.Setup(x => x.FindByNameAsync(existingUsernames[1])).ReturnsAsync(new ApplicationUser { UserName = existingUsernames[1] });

            // Act
            _sut = new UsernameHelper(_userManagerMock.Object);
            result = await _sut.GenerateUniqueUsernameAsync(firstname, lastname);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
