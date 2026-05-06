using CondoAmenitiesBooking.Application.Common;
using CondoAmenitiesBooking.Application.Features.Bookings.Commands;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.Bookings.Handlers
{
    public class CancelBookingHandler
    {
        private readonly IBookingService _service;
        private readonly IAuditService _auditService;
        private readonly IUserService _userService;

        public CancelBookingHandler(IBookingService service, IAuditService auditService, IUserService userService)
        {
            _service = service;
            _auditService = auditService;
            _userService = userService;
        }

        public async Task<Result> Handle(CancelBookingCommand cmd)
        {
            var user = await _userService.GetById(cmd.UserId);
            var booking = await _service.CancelBooking(cmd.BookingId, cmd.UserId);

            if (booking == null)
                return Result.Failure("Cannot cancel booking (rule violation or not found)");

            // Format user info
            var userInfo = $"{user.FirstName} {user.LastName} " +
               $"(Block {user.Block}, Floor {user.Floor:D2}, Unit {user.Unit:D2})";

            // Audit message
            var details = $"{userInfo} CANCELLED booking (BookingId={booking.BookingId}) " +
              $"for {booking.Amenity.Name} " +
              $"originally scheduled on {booking.StartTime:yyyy-MM-dd} " +
              $"from {booking.StartTime:hh:mm tt} to {booking.EndTime:hh:mm tt}. " +
              $"Cancelled at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";

            // Audit Log
            await _auditService.LogAsync(
                cmd.UserId,
                "CANCEL",
                "Booking",
                details
            );

            return Result.Success();
        }
    }
}
