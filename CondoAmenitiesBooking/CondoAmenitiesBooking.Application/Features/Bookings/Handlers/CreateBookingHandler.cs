using CondoAmenitiesBooking.Application.Common;
using CondoAmenitiesBooking.Application.Features.Bookings.Commands;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Application.Features.Bookings.Handlers
{
    public class CreateBookingHandler
    {
        private readonly IBookingService _bookingService;
        private readonly IEmailService _emailService;

        public CreateBookingHandler(
            IBookingService bookingService,
            IEmailService emailService)
        {
            _bookingService = bookingService;
            _emailService = emailService;
        }

        public async Task<Result> Handle(CreateBookingCommand cmd)
        {
            if (cmd.StartTime >= cmd.EndTime)
                return Result.Failure("Invalid time range");

            var hasConflict = await _bookingService.HasConflict(
                cmd.AmenityId,
                cmd.StartTime,
                cmd.EndTime);

            if (hasConflict)
                return Result.Failure("Time slot already booked");

            var booking = new Booking
            {
                UserId = cmd.UserId,
                AmenityId = cmd.AmenityId,
                StartTime = cmd.StartTime,
                EndTime = cmd.EndTime,
                Status = BookingStatus.Confirmed
            };

            var saved = await _bookingService.CreateBooking(booking);

            await _emailService.SendAsync(
                "Booking Confirmed",
                $"Booking #{saved.BookingId} confirmed");

            return Result.Success(saved.BookingId);
        }
    }
}
