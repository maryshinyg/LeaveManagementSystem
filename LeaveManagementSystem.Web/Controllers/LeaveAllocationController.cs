using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Services.LeaveAllocations;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Web.Controllers;

[Authorize]
public class LeaveAllocationController(ILeaveAllocationService _leaveAllocationService,
    ILeaveTypeService _leaveTypeService) : Controller
{
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Index()
    {
        var employees = await _leaveAllocationService.GetEmployees();
        return View(employees);
    }

    [Authorize(Roles = Roles.Administrator)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AllocateLeave(string? id)
    {
        await _leaveAllocationService.AllocateLeave(id);
        return RedirectToAction(nameof(Details), new {userId = id});
    }

    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> EditAllocation(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var allocation = await _leaveAllocationService.GetEmployeeAllocation(id.Value);
        if (allocation == null)
        {
            return NotFound();
        }
        return View(allocation);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocation)
    {
        if (await _leaveTypeService.DaysExceededMaximum(allocation.LeaveType.Id, allocation.NumberOfDays))
        {
            ModelState.AddModelError("NumberOfDays", "The allocation exceeds the maximum leave type value");
        }

        if (ModelState.IsValid)
        {
            await _leaveAllocationService.EditAllocation(allocation);
            return RedirectToAction(nameof(Details), new { userId = allocation.Employee.Id });
        }

        var days = allocation.NumberOfDays;
        allocation = await _leaveAllocationService.GetEmployeeAllocation(allocation.Id);
        allocation.NumberOfDays = days;
        return View(allocation);
    }

    public async Task<IActionResult> Details(string? UserId)
    {
        var employeeVm = await _leaveAllocationService.GetEmployeeAllocations(UserId);
        return View(employeeVm);
    }

}
