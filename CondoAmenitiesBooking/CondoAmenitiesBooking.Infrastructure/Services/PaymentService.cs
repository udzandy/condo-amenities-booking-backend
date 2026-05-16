using CondoAmenitiesBooking.Application.Common;
using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Domain.Enums;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IAuditService _auditService;

        public PaymentService(AppDbContext context, IUnitOfWork unitOfWork, IEmailService emailService, IAuditService auditService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _auditService = auditService;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        // =====================================================
        // GET ADMIN PAYMENT TABLE
        // =====================================================

        public async Task<List<AdminPaymentDto>> GetAdminPayments()
        {
            var data = await
                (from b in _context.Bookings
                 join u in _context.Users on b.UserId equals u.UserId
                 join a in _context.Amenities on b.AmenityId equals a.AmenityId
                 join un in _context.AmenityUnits on b.UnitId equals un.UnitId
                 join s in _context.AmenityTimeSlots on b.SlotId equals s.SlotId
                 join p in _context.Payments on b.BookingId equals p.BookingId
                 select new AdminPaymentDto
                 {
                     BookingId = b.BookingId,
                     UserName = u.FirstName + " " + u.LastName,
                     AmenityName = a.Name,
                     UnitName = un.UnitName,
                     SlotTime = s.StartTime + " - " + s.EndTime,
                     BookingDate = b.BookingDate,
                     Amount = p.Amount,
                     PaymentMethod = p.PaymentMethod.ToString(),
                     PaidDate = p.PaidAt,
                     IsPaid = p.IsPaid,
                     BookingStatus = b.BookingStatus.ToString()
                 }).ToListAsync();

            return data;
        }

        // =====================================================
        // PAY BOOKING
        // =====================================================

        public async Task<Result> PayBooking(PayBookingRequestDto request)
        {
            //using var transaction =await _context.Database.BeginTransactionAsync();
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(x => x.BookingId == request.BookingId);

                if (payment == null)
                    return Result.Failure("Payment not found");

                var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == request.BookingId);

                if (booking == null)
                    return Result.Failure("Booking not found");

                if (payment.IsPaid)
                    return Result.Failure("Already paid");

                // UPDATE PAYMENT

                payment.IsPaid = true;
                //payment.PaymentMethod = Enum.Parse<PaymentMethod>(request.PaymentMethod, ignoreCase: true);
                payment.PaymentMethod = (PaymentMethod)request.PaymentMethod;
                payment.PaidAt = DateTime.UtcNow;
                payment.Amount = request.Amount;
                payment.TransactionId = Guid.NewGuid();
                payment.Status = PaymentStatus.Paid;

                // UPDATE BOOKING

                booking.BookingStatus = BookingStatus.Confirmed;

                await _context.SaveChangesAsync();

                // GET USER

                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == booking.UserId);

                // AUDIT DETAILS

                //var adminUserId = "01-00-01";

                var details = $"{user.FirstName} {user.LastName} successfully paid. " +
                              $"(BookingId: {booking.BookingId}) | " +
                              $"Method: {payment.PaymentMethod} | " +
                              $"Amount: SGD {payment.Amount:C} | " + // :C formats as currency (e.g., $100.00)
                              $"Date: {payment.PaidAt:dd MMM yyyy hh:mm tt}";

                // AUDIT LOG
                await _auditService.LogAsync(
                    user.UserId,
                    "Payment",
                    "Booking",
                    details
                );


                //await transaction.CommitAsync();
                await _unitOfWork.CommitAsync();

                // SEND EMAIL AFTER COMMIT

                if (user != null)
                {
                    await _emailService.SendAsync(
                        user.Email,
                        "Payment Successful",
                        $@"
                        <h2>Payment Successful</h2>

                        <p>Hello {user.FirstName},</p>

                        <p>
                            Your payment for booking
                            #{booking.BookingId}
                            has been completed.
                        </p>

                        <table border='1' cellpadding='8'>

                            <tr>
                                <td><b>Payment Method</b></td>
                                <td>{payment.PaymentMethod}</td>
                            </tr>

                            <tr>
                                <td><b>Amount</b></td>
                                <td>{payment.Amount}</td>
                            </tr>

                            <tr>
                                <td><b>Paid Date</b></td>
                                <td>
                                    {payment.PaidAt:dd MMM yyyy hh:mm tt}
                                </td>
                            </tr>

                        </table>

                        <br/>

                        <p>Thank you.</p>
                    ");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(ex.Message);
            }
        }
    }
}
