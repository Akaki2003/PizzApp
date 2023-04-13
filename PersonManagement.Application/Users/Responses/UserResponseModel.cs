﻿using PizzApp.Application.Addresses.Responses;
using PizzApp.Domain.Addresses;

namespace PizzApp.Application.Users.Responses
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<AddressResponseModel> Addresses { get; set; }

        
    }

   
}
