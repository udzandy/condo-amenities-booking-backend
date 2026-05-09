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
            // VALIDATE USER
            var user = await _userService.GetById(cmd.UserId);
            if (user == null)
                return Result.Failure("User not found");

            // VALIDATE AMENITY
            var amenity = await _amenityService.GetById(cmd.AmenityId);
            if (amenity == null)
                return Result.Failure("Amenity not found");

            // GET UNIT
            var unit = await _amenityService.GetUnitById(cmd.UnitId);
            if (unit == null)
                return Result.Failure("Amenity unit not found");

            // GET SLOT
            var slot = await _amenityService.GetSlotById(cmd.SlotId);
            if (slot == null)
                return Result.Failure("Time slot not found");

            //if (cmd.StartTime >= cmd.EndTime)
            //    return Result.Failure("Invalid time range");

            // CONFLICT CHECK
            var hasConflict = await _bookingService.HasConflict(cmd.UnitId, cmd.SlotId, cmd.BookingDate);
            if (hasConflict)
                return Result.Failure("Selected slot already booked");

            // CREATE BOOKING
            var booking = new Booking
            {
                UserId = cmd.UserId,
                AmenityId = cmd.AmenityId,
                UnitId = cmd.UnitId,
                SlotId = cmd.SlotId,
                BookingDate = cmd.BookingDate.Date,
                Status = BookingStatus.Confirmed
            };

            var saved = await _bookingService.CreateBooking(booking);

            // FORMAT USER INFO
            var userInfo =
                $"{user.FirstName} {user.LastName} " +
                $"(Block {user.Block}, " +
                $"Floor {user.Floor:D2}, " +
                $"Unit {user.Unit:D2})";

            // FORMAT SLOT TIME
            var slotTime =
                $"{DateTime.Today.Add(slot.StartTime):hh:mm tt} - " +
                $"{DateTime.Today.Add(slot.EndTime):hh:mm tt}";

            // AUDIT DETAILS
            var details =
                $"{userInfo} CREATED booking " +
                $"(BookingId={saved.BookingId}) " +
                $"for {amenity.Name} - {unit.UnitName} " +
                $"on {cmd.BookingDate:yyyy-MM-dd} " +
                $"during {slotTime} " +
                $"at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC";

            // AUDIT LOG
            await _auditService.LogAsync(
                cmd.UserId,
                "CREATE",
                "Booking",
                details
            );

            // EMAIL
            var emailMessage = $"Your booking #{saved.BookingId} for " +
                $"{amenity.Name} ({unit.UnitName}) on " +
                $"{cmd.BookingDate:yyyy-MM-dd} during {slotTime} " +
                $"has been confirmed.";

            await _emailService.SendAsync(
                "Booking Confirmed",
                emailMessage);

            return Result.Success(saved.BookingId);
        }
    }
}
