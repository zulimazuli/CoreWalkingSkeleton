using System;
using CoreTemplate.Web.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace UnitTests.Validators
{
    [TestFixture]
    public class ItemValidatorTester
    {
        private ItemValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ItemValidator();
        }


        [Test]
        public void ShouldHaveError_GuidIsEmpty()
        {
            _sut.ShouldHaveValidationErrorFor(item => item.UniqueId, Guid.Empty);
        }


        [Test]
        public void ShouldNotHaveError_GuidIsValid()
        {
            _sut.ShouldNotHaveValidationErrorFor(item => item.UniqueId, Guid.NewGuid());
        }
    }


}