using Bogus;
using FluentValidation.TestHelper;
using UniversalClientBase.Application.Dtos;
using UniversalClientBase.Application.Validators;

public class CreateContactValidatorTests {
    private readonly CreateContactValidator _validator = new();
    private readonly Faker _faker = new();

    [Fact]
    public void Validator_ShouldFail_WhenFirstNameIsTooLong() {
        var dto = new CreateContactDto(_faker.Random.String2(51), "Perez", "test@test.com", "123", "Prospecto");

        // Act & Assert
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenEmailIsInvalid()
    {
        var dto = new CreateContactDto( "Juan", "Perez", "email-invalido", "123", "Prospecto");
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
}