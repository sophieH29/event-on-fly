﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOnFly.Data.DbModels
{
    public class Vendor
    {
      public int Id { get; set; }
      public string IdentityId { get; set; }
      public AppUser Identity { get; set; }  // navigation property
      public string Location { get; set; }
  }
}
