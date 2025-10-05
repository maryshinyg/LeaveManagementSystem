using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveType : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "Name must be 1 to 50 letters only.")]
        public string Name { get; set; }
        public int NumberOfDays { get; set; }

        public List<LeaveAllocation>? LeaveAllocations { get; set; }
    }
}
