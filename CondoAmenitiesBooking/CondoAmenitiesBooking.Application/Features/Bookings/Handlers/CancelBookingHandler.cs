using CondoAmenitiesBooking.Application.Common;
using CondoAmenitiesBooking.Application.Features.Bookings.Commands;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Features.Bookings.Handlers
{
    public class CancelBookingHandler
    {
        private readonly IBookingService _service;
        private readonly IAuditService _auditService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public CancelBookingHandler(IBookingService service, IAuditService auditService, IUserService userService, IEmailService emailService)
        {
            _service = service;
            _auditService = auditService;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<Result> Handle(CancelBookingCommand cmd)
        {
            var user = await _userService.GetById(cmd.UserId);
            var booking = await _service.CancelBooking(cmd.BookingId, cmd.UserId);

            if (booking.Item1 == null)
                return Result.Failure("Cannot cancel booking (rule violation or not found)");

            // Format user info
            var userInfo = $"{user.FirstName} {user.LastName} " +
               $"(Block {user.Block}, Floor {user.Floor:D2}, Unit {user.Unit:D2})";

            // Audit message
            var details = $"{userInfo} CANCELLED booking (BookingId={booking.Item1.BookingId}) " +
              $"for {booking.Item1.Amenity.Name} "; //+
              //$"originally scheduled on {booking.StartTime:yyyy-MM-dd} " +
              //$"from {booking.StartTime:hh:mm tt} to {booking.EndTime:hh:mm tt}. " +
              //$"Cancelled at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";

            // Audit Log
            await _auditService.LogAsync(
                cmd.UserId,
                "CANCEL",
                "Booking",
                details
            );

            await _emailService.SendAsync(
            user.Email,
            "Booking Cancelled",
            $@"
                <h2>Booking Cancelled</h2>

                <p>Hello {user.FirstName},</p>

                <p>Your booking has been cancelled successfully.</p>

                <table border='1' cellpadding='8'>
                    <tr>
                        <td><b>Booking ID</b></td>
                        <td>{booking.Item1.BookingId}</td>
                    </tr>

                    <tr>
                        <td><b>Amenity</b></td>
                        <td>{booking.Item1.Amenity.Name}</td>
                    </tr>

                    <tr>
                        <td><b>Booking Date</b></td>
                        <td>{booking.Item1.BookingDate:dd MMM yyyy}</td>
                    </tr>

                    <tr>
                        <td><b>Status</b></td>
                        <td>Cancelled</td>
                    </tr>
                </table>

                <br/>

                <p>If this was not done by you, please contact management immediately.</p>

                <br/>

                <p>Thank you.</p>
            ");

            return Result.Success();
        }
    }
}
