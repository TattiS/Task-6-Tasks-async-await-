using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
    public class DepartureValidator : AbstractValidator<DepartureDTO>
    {
		public DepartureValidator()
		{
			RuleFor(p => p.Id).Empty();
			RuleFor(p => p.FlightId).NotNull().NotEmpty();
			RuleFor(p => p.DepartureDate).NotNull().NotEmpty();
			RuleFor(p=> p.CrewItem).NotNull();
			RuleFor(p => p.PlaneItem).NotNull();
		}
    }
}
