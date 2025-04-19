using Microsoft.AspNetCore.Identity;

namespace StrokePrediction.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<LabTest> LabTests { get; set; }
    }
}
