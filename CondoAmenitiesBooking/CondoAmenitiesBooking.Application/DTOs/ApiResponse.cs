namespace CondoAmenitiesBooking.Application.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public T? Data { get; set; }
        public int StatusCode { get; set; }
    }
}
