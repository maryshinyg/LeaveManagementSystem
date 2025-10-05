namespace LeaveManagementSystem.Web.Data
{
    public class LeaveAllocation : BaseEntity
    {
        public LeaveType? LeaveType { get; set; } //FK to LeaveTypes table
        public int LeaveTypeId { get; set; }

        public ApplicationUser Employee { get; set; } //FK to AspNetUsers table
        public string EmployeeId { get; set; }

        public Period Period { get; set; } //FK to Periods table
        public int PeriodId { get; set; }

        public int NumberOfDays { get; set; }
    }
}
