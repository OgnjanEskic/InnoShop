using Domain.Models.Requests;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Represents the validation class of the Location request object.
    /// </summary>
    public class AddLocationRequestValidator : AbstractValidator<LocationRequest>
    {
        /// <summary>
        /// Constructor for AddLocationRequestValidator class to validate Location request model input.
        /// </summary>
        public AddLocationRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 50);
            RuleFor(x => x.Description).Length(0, 500);
        }
    }
}
