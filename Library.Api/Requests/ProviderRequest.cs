﻿using System.Runtime.Serialization;
using Library.Core.Concrete.Models;

namespace Library.Api.Requests
{
  
    public class ProviderRequest
    {
       
        public int Id { get; set; }
      
        public string Name { get; set; }

        public int OriginCountryId { get; set; }
       

    }
}