using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
	public class FlightValidator:AbstractValidator<FlightDTO>
    {
		public FlightValidator()
		{
			RuleFor(p => p.Id).Empty();
			RuleFor(p => p.DeparturePoint).NotEmpty().NotNull().NotEqual(p=>p.Destination);
			RuleFor(p => p.DepartureTime).NotEqual(p => p.ArrivalTime);
			RuleFor(p => p.Destination).NotEmpty().NotNull().NotEqual(p => p.DeparturePoint);
			RuleFor(p => p.Tickets).NotNull();
		}
    }
}
