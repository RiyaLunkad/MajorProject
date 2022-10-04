using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CrimeMgmnt.Models
{
    [Table(name:"ControlRoom")]
    public class CyberCell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int  ControlRoomId { get; set; }

        #region Navigation Properties to the User Model

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(CyberCell.UserId))]
        public User Users { get; set; }

        #endregion

        [Required]
        [DefaultValue(false)]
        [Display(Name ="Processed Or Not")]
        public bool IsEnabled { get; set; }


        [Required]
        [StringLength(100)]
        public string Status { get; set; }


    }
}
