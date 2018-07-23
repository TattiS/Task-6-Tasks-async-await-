using DTOLibrary.DTOs;
using FluentValidation;

namespace AirportService.Validators
{
	public class TicketValidator : AbstractValidator<TicketDTO>
    {
		public TicketValidator()
		{
			RuleFor(p=>p.Id).Empty();
			RuleFor(p => p.Price).NotNull().NotEmpty();
			RuleFor(p => p.FlightId).NotNull().NotEmpty();
		}
    }
}
