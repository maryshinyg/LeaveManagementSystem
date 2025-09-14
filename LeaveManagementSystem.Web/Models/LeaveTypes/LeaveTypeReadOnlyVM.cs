namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM : BaseTypeVM
    {
        public string Name { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
    }
}
