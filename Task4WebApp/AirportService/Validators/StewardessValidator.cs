using System;
using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
	public class StewardessValidator:AbstractValidator<StewardessDTO>
    {
		public StewardessValidator()
		{
			RuleFor(p => p.Id).Empty();
			RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(50);
			RuleFor(p => p.Surname).NotNull().NotEmpty().MaximumLength(50);
			RuleFor(p => p.BirthDate).NotNull().LessThan(DateTime.Now);
		}
    }
}
