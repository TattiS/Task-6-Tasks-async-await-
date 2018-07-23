using System;
using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
	public class PilotValidator:AbstractValidator<PilotDTO>
    {
		public PilotValidator()
		{
			RuleFor(p => p.Id).Empty();
			RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(50);
			RuleFor(p => p.Surname).NotNull().NotEmpty().MaximumLength(50);
			RuleFor(p => p.BirthDate).LessThan(DateTime.Now);
		}
    }
}
