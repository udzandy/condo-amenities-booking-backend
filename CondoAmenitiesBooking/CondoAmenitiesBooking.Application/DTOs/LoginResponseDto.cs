using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoAmenitiesBooking.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
