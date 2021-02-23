using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.WebApi.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Validators
{
    public class ValidatorsTests
    {
        [Fact]
        public void AddressValidatorTest()
        {
            var validator = new AddressValidator();

            var validate = validator
                .TestValidate(new Address()
                {
                    City = "City",
                    Country = "Country",
                    Street = "Street",
                    Location = new Location()
                    {
                        Longitude = 0,
                        Latitude = 0
                    }
                });

            validate.ShouldNotHaveValidationErrorFor(v => v.City);
            validate.ShouldNotHaveValidationErrorFor(v => v.Country);
            validate.ShouldNotHaveValidationErrorFor(v => v.Street);
            validate.ShouldNotHaveValidationErrorFor(v => v.Location);
        }

        [Fact]
        public void CompanyValidatorTest()
        {
            var validator = new CompanyValidator();

            var validate = validator
                .TestValidate(new Company()
                {
                    Description = "Description",
                    Mail = "sobaki@tut.net",
                    Name = "Name",
                    PhoneNumber = "375289637568"
                });

            validate.ShouldNotHaveValidationErrorFor(v => v.Description);
            validate.ShouldNotHaveValidationErrorFor(v => v.Mail);
            validate.ShouldNotHaveValidationErrorFor(v => v.Name);
            validate.ShouldNotHaveValidationErrorFor(v => v.PhoneNumber);
        }

        [Fact]
        public void SearchAdvancedCriteriaValidatorTest()
        {
            var searchAmountOfDiscountCriteriaValidator = new SearchAmountOfDiscountCriteriaValidator();
            var validateSearchAmountOfDiscountCriteriaValidator = searchAmountOfDiscountCriteriaValidator
                .TestValidate(new SearchAmountOfDiscountCriteria()
                {
                    SearchAmountOfDiscountMin = 1,
                    SearchAmountOfDiscountMax = 100
                });

            validateSearchAmountOfDiscountCriteriaValidator.ShouldNotHaveAnyValidationErrors();

            var searchRatingTotalCriteriaValidator = new SearchRatingTotalCriteriaValidator();
            var validateSearchRatingTotalCriteriaValidator = searchRatingTotalCriteriaValidator
                .TestValidate(new SearchRatingTotalCriteria()
                {
                    SearchRatingTotalMin = 1,
                    SearchRatingTotalMax = 5
                });

            validateSearchRatingTotalCriteriaValidator.ShouldNotHaveAnyValidationErrors();

            var searchAdvancedCriteriaValidator =
                new SearchAdvancedCriteriaValidator(searchAmountOfDiscountCriteriaValidator, searchRatingTotalCriteriaValidator);

            var searchAdvancedCriteriaCompany = new SearchAdvancedCriteria()
            {
                CompanyName = "CompanyName",
                SearchAmountOfDiscount = new SearchAmountOfDiscountCriteria()
                {
                    SearchAmountOfDiscountMin = 1,
                    SearchAmountOfDiscountMax = 100
                },
                SearchRatingTotal = new SearchRatingTotalCriteria()
                {
                    SearchRatingTotalMin = 1,
                    SearchRatingTotalMax = 5
                },
                SearchDate = new SearchDateCriteria()
                {
                    SearchStartDate = "01.01.2021 10:10:10".GetUtcDateTime().ToUniversalTime(),
                    SearchEndDate = DateTime.Now.ToUniversalTime()
                }
            };

            var validateSearchAdvancedCriteriaValidatorCompany = searchAdvancedCriteriaValidator
                .TestValidate(searchAdvancedCriteriaCompany);

            validateSearchAdvancedCriteriaValidatorCompany.ShouldNotHaveValidationErrorFor(v => v.CompanyName);
            validateSearchAdvancedCriteriaValidatorCompany.ShouldNotHaveValidationErrorFor(v => v.SearchAmountOfDiscount);
            validateSearchAdvancedCriteriaValidatorCompany.ShouldNotHaveValidationErrorFor(v => v.SearchDate);
            validateSearchAdvancedCriteriaValidatorCompany.ShouldNotHaveValidationErrorFor(v => v.SearchRatingTotal);

            var searchAdvancedCriteriaValidator2 =
                new SearchAdvancedCriteriaValidator(searchAmountOfDiscountCriteriaValidator, searchRatingTotalCriteriaValidator);

            var searchAdvancedCriteria = new SearchAdvancedCriteria()
            {
                CompanyName = null,
                SearchAmountOfDiscount = new SearchAmountOfDiscountCriteria()
                {
                    SearchAmountOfDiscountMin = 1,
                    SearchAmountOfDiscountMax = 100
                },
                SearchRatingTotal = new SearchRatingTotalCriteria()
                {
                    SearchRatingTotalMin = 1,
                    SearchRatingTotalMax = 5
                },
                SearchDate = new SearchDateCriteria()
                {
                    SearchStartDate = "01.01.2021 10:10:10".GetUtcDateTime(),
                    SearchEndDate = DateTime.UtcNow
                }
            };

            var validateSearchAdvancedCriteriaValidator = searchAdvancedCriteriaValidator2
                .TestValidate(searchAdvancedCriteria);

            validateSearchAdvancedCriteriaValidator.ShouldNotHaveAnyValidationErrors();

            var searchCriteriaValidator = new SearchCriteriaValidator(searchAdvancedCriteriaValidator);

            var validateSearchCriteriaValidator = searchCriteriaValidator
                .TestValidate(new SearchCriteria()
                {
                    SearchText = "",
                    SearchLanguage = LanguageOption.Ru,
                    SearchAddressCity = "SearchAddressCity",
                    SearchAddressCountry = "SearchAddressCountry",
                    SearchDiscountOption = DiscountOption.All,
                    SearchPaginationCountElementPerPage = 5,
                    SearchPaginationPageNumber = 1,
                    SearchShowDeleted = false,
                    SearchSortFieldOption = SortFieldOption.DateStart,
                    SearchSortOption = SortOption.Asc,
                    IncomingUser = new IncomingUser()
                    {
                        Id = Guid.Parse("82cabda2-2e10-4fe5-a78f-ade3bcb6d854"),
                        Role = RoleOption.Employee
                    },
                    SearchAdvanced = searchAdvancedCriteriaCompany
                });

            validateSearchCriteriaValidator.ShouldNotHaveValidationErrorFor(v => v.SearchAdvanced);
            validateSearchCriteriaValidator.ShouldNotHaveValidationErrorFor(v => v.SearchLanguage);
            validateSearchCriteriaValidator.ShouldNotHaveValidationErrorFor(v => v.SearchText);
            validateSearchCriteriaValidator.ShouldNotHaveValidationErrorFor(v => v.SearchPaginationCountElementPerPage);
        }

        [Fact]
        public void UserValidatorTest()
        {
            var validator = new UserValidator();

            var validate = validator
                .TestValidate(new User()
                {
                    Id = Guid.Parse("3140cc24-95d3-4d67-9c59-9af17be4a49a"),
                    Name = "Name",
                    Surname = "Surname",
                    Mail = "sobaki@tut.net",
                    PhoneNumber = "35689125365",
                    Roles = RoleOption.Employee,
                    HashPassword = "",
                    Salt = "Salt"
                });

            validate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void VoteValueValidatorTest()
        {
            var validator = new VoteValueValidator();

            var validate = validator
                .Validate(1);

            validate.IsValid.Should().BeTrue();
        }

        [Fact]
        public void SearchStringValidatorTest()
        {
            var validator = new SearchStringValidator();

            var validate = validator
                .Validate("found");

            validate.IsValid.Should().BeTrue();
        }

        [Fact]
        public void UpsertDiscountValidatorTest()
        {

            var validator = new UpsertDiscountValidator(new CompanyValidator(), new AddressValidator(), new PromocodeOptionsValidator());

            var validate = validator
                .Validate(
                    new UpsertDiscountRequest()
                    {
                        Id = Guid.Parse("0597cebd-d159-492f-aede-671752ce5dd8"),
                        Name = "Name",
                        Company = null,
                        Address = null,
                        Language = LanguageOption.Ru,
                        AmountOfDiscount = 50,
                        Description = "Desc",
                        Translations = null,
                        Tags = null,
                        StartDate = "01.01.2021 10:10:10".GetUtcDateTime(),
                        EndDate = DateTime.UtcNow,
                        WorkingDaysOfTheWeek = "0101011"
                    }
                    );

            validate.IsValid.Should().BeTrue();
        }
    }
}
