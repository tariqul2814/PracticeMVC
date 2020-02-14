using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyTutorial.Models.ViewModel
{
    public class NewCustomerViewModel
    {
        public Customer customer { get; set; }
        public IEnumerable <MembershipType> membershipType { get; set; }
    }
}