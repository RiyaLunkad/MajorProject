using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CrimeMgmnt.Models
{
    [Table(name: "Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <returns>This will return Auto generated id</returns>
        /// <paramname="UserId">description</param>
        public int UserId { get; set; }                                               

        [Required(ErrorMessage ="{0} cannot be empty!")]
        [StringLength(100)]
        [Display(Name = "UserName")]
        ///<returns>Name of User</returns>
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must enter the Phone number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile No")]
        [Display(Name = "Phone Number")]
        ///<returns>Phone No of User</returns>
        public string mobile { get; set; }

        [Required]
        [Range(1,100)]
        ///<returns>Age of User</returns>
        public int Age { get; set; }

        [Required]
        //[MinLength(2, ErrorMessage = "{0} cannot have lesser than {2} characters")]
        [Display(Name ="Platform of crime")]
        public string CrimePlatform {get; set;}

        [Required]
        [StringLength(50)]
        //[MaxLength(2, ErrorMessage = "{0} cannot have more than {100} characters")]
        [Display(Name ="Brief description of crime")]
        public String CrimeDescription { get; set; }

        [Display(Name = "Date of Crime")]
        [DataType(DataType.Date)]
        ///<returns>Only Date is returned</returns>
        public DateTime? DateTime { get; set; }

        //[Required]
        //[Display(Name = "Date And Time of Crime")]
        //public DateTime DateTime { get; set; }

        [Required]
        [Display(Name ="Current city ")]
        ///<returns>City is returned</returns>
        public string Place { get; set; }

        #region Navigation Properties to the ControlRoom Model
        [JsonIgnore]        // It ensures to suppress the informaation about the foreign key realtionship.
        public ICollection<CyberCell> ControlRooms { get; set; }

        #endregion

    }
}
