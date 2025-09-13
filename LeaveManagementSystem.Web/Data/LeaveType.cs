using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveType
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "Name must be 1 to 50 letters only.")]
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
    }
}
