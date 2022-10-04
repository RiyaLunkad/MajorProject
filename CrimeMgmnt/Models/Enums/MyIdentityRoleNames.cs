using System.ComponentModel.DataAnnotations;

namespace CrimeMgmnt.Models
{
    public enum MyIdentityRoleNames
    {
        [Display(Name = "Authority Role")]
        AppAdmin,

        [Display(Name = "User Role")]
        AppUser

    }
}
