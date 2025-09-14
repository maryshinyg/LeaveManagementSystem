using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeEditVM : BaseTypeVM
    {

        [Required]
        [Length(4, 150, ErrorMessage = "You have voilated the length requirements")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 90, ErrorMessage = "Please enter a valid number between 1 and 25")]
        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }

}
