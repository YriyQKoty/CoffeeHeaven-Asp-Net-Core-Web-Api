﻿using System.Runtime.Serialization;
using Library.Core.Concrete.Models;

namespace Library.Api.Responses
{
    [DataContract]
    public class CoffeeResponse
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }
  
        public string Description { get; set; }
    
        public CoffeeType Type { get; set; }
   
        public CoffeeQuality Quality { get; set; }
        
        public int ProviderId { get; set; }
        

    }
}