using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
    public class CrewValidator:AbstractValidator<CrewDTO>
    {
		public CrewValidator()
		{
			RuleFor(p => p.Id).Empty();
			RuleFor(p => p.PilotId).NotNull().GreaterThan(0);
			RuleFor(p => p.Stewardesses).NotNull();
			
		}
    }
}
