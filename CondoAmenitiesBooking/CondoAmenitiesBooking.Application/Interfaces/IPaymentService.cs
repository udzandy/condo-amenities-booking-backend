using CondoAmenitiesBooking.Application.Common;
using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<Result> PayBooking(PayBookingRequestDto request);
        Task<List<AdminPaymentDto>> GetAdminPayments();
    }
}
