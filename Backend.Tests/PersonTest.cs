using Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Backend.Tests
{
    public class PersonTest
    {
        [Fact]
        public void Person_WithNameAndEmail_IsValid()
        {
            var person = new Person()
            {
                Name = "SomeName",
                Email = "SomeEmail@email.com"
            };

            var validationContext = new ValidationContext(person);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(person, validationContext, validationResults);

            Assert.True(result);
        }

        [Fact]
        public void Person_WithoutName_IsNotValid()
        {
            var person = new Person()
            {
                Name = "",
                Email = "SomeEmail@email.com"
            };

            var validationContext = new ValidationContext(person);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(person, validationContext, validationResults);

            Assert.False(result);
        }
    }
}
