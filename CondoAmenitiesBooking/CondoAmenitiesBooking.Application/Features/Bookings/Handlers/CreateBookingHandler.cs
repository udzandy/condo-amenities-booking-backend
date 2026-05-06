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
        private readonly IAuditService _auditService;
        private readonly IAmenityService _amenityService;
        private readonly IUserService _userService;

        public CreateBookingHandler(
            IBookingService bookingService,
            IEmailService emailService,
            IAuditService auditService,
            IAmenityService amenityService,
            IUserService userService)
        {
            _bookingService = bookingService;
            _emailService = emailService;
            _auditService = auditService;
            _amenityService = amenityService;
            _userService = userService;
        }

        public async Task<Result> Handle(CreateBookingCommand cmd)
        {
            var user = await _userService.GetById(cmd.UserId);
            if (user == null)
                return Result.Failure("User not found");

            if (cmd.StartTime >= cmd.EndTime)
                return Result.Failure("Invalid time range");

            var hasConflict = await _bookingService.HasConflict(
                cmd.AmenityId,
                cmd.StartTime,
                cmd.EndTime);

            if (hasConflict)
                return Result.Failure("Time slot already booked");

            var amenity = await _amenityService.GetById(cmd.AmenityId);
            if (amenity == null)
                return Result.Failure("Amenity not found");

            var booking = new Booking
            {
                UserId = cmd.UserId,
                AmenityId = cmd.AmenityId,
                StartTime = cmd.StartTime,
                EndTime = cmd.EndTime,
                Status = BookingStatus.Confirmed
            };

            var saved = await _bookingService.CreateBooking(booking);

            // Format user info
            var userInfo = $"{user.FirstName} {user.LastName} " +
               $"(Block {user.Block}, Floor {user.Floor:D2}, Unit {user.Unit:D2})";

            // Audit message
            var details = $"{userInfo} CREATED booking (BookingId={saved.BookingId}) " +
                          $"for {amenity.Name} " +
                          $"on {cmd.StartTime:yyyy-MM-dd} " +
                          $"from {cmd.StartTime:hh:mm tt} to {cmd.EndTime:hh:mm tt} " +
                          $"at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";

            // AUDIT LOG
            await _auditService.LogAsync(
                cmd.UserId,
                "CREATE",
                "Booking",
                details
            );

            await _emailService.SendAsync(
                "Booking Confirmed",
                $"Booking #{saved.BookingId} confirmed");

            return Result.Success(saved.BookingId);
        }
    }
}
