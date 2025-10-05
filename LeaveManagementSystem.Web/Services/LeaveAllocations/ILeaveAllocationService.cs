using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public interface ILeaveAllocationService
{
    Task AllocateLeave(string employeeId);
    Task<EmployeeAllocationVM> GetEmployeeAllocations(string? UserId);
    Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId);
    Task<List<EmployeeListVM>> GetEmployees();
    Task EditAllocation(LeaveAllocationEditVM allocationEditVm);
}
