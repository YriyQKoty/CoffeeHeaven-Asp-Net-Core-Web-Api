using System.Collections.Generic;

namespace Library.Api.Requests
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
        

        public string PasswordConfirm { get; set; }
        
    }
}