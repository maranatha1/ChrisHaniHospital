using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChrisHaniHospital.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ChrisHaniUser class
public class ChrisHaniUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? IdentityNumber { get; set; }
}
