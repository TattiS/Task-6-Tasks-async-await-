using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
    public class PlaneValidator:AbstractValidator<PlaneDTO>
    {
		public PlaneValidator()
		{
			RuleFor(p => p.Id).Empty();
			RuleFor(p => p.Name).NotNull().NotEmpty();
			RuleFor(p => p.TypeOfPlane).NotNull().NotEmpty();
			RuleFor(p => p.ReleaseDate).NotNull().NotEmpty();
			RuleFor(p => p.OperationLife).NotEmpty().NotNull();
		}
    }
}
