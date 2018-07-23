using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
	public class PlaneTypeValidator:AbstractValidator<PlaneTypeDTO>
    {
		public PlaneTypeValidator()
		{
			RuleFor(p => p.Id).Empty();
			RuleFor(p => p.Model).NotNull().NotEmpty().MaximumLength(50);
			RuleFor(p => p.Seats).NotNull().NotEmpty().GreaterThan(0);
			RuleFor(p => p.AirLift).NotNull().NotEmpty().GreaterThan(0);
		}
    }
}
