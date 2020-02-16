using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Helpers;
using CoreTemplate.ApplicationCore.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using UnitTests.Shared;

namespace UnitTests.Helpers
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [TestFixture]
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
            var firstname = "Jonathan";
            var lastname = "Yehonathan";

            var expectedResult = "jyehonathan3";
            
            var existingUsernames = new[] { "jyehonathan", "jyehonathan1", "jyehonathan2" };
            foreach (var username in existingUsernames)
            {
                _userManagerMock.Setup(x => x.FindByNameAsync(username)).ReturnsAsync(new ApplicationUser { UserName = username });
            }
            
            // Act
            _sut = new UsernameHelper(_userManagerMock.Object);
            result = await _sut.GenerateUniqueUsernameAsync(firstname, lastname);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
