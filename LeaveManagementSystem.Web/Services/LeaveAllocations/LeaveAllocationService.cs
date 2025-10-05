
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations;

public class LeaveAllocationService(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor, UserManager<ApplicationUser> _userManager, IMapper _mapper) : ILeaveAllocationService
{
    public async Task AllocateLeave(string employeeId)
    {
        // get all the leave types
        var leaveTypes = await _context.LeaveTypes.
            Where(q => !q.LeaveAllocations.Any(x => x.EmployeeId == employeeId))
            .ToListAsync();

        // get the current period based on the year
        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
        var MonthsRemaining = period.EndDate.Month - currentDate.Month;

        // foreach leave type, create an allocation entry
        foreach (var leaveType in leaveTypes)
        {
            // Works, but not best practice
            //var allocationExists = await AllocationExists(employeeId, period.Id, leaveType.Id);
            //if (allocationExists) continue;

            var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
            var leaveAllocation = new LeaveAllocation
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                NumberOfDays = (int)Math.Ceiling(accuralRate * MonthsRemaining),
            };
            _context.Add(leaveAllocation);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
    {
        var user = string.IsNullOrEmpty(userId) ? await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User) : await _userManager.FindByIdAsync(userId);

        var allocations = await GetAllocations(user.Id);
        var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
        var leaveTypesCount = await  _context.LeaveTypes.CountAsync();

        var employeeVm = new EmployeeAllocationVM
        {
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationVmList,
            IsCompletedAllocation = leaveTypesCount == allocations.Count()
        };

        return employeeVm;
    }

    public async Task<List<EmployeeListVM>> GetEmployees()
    {
        var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
        var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());
        return employees;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
    {
        var allocation = await _context.LeaveAllocations
               .Include(q => q.LeaveType)
               .Include(q => q.Employee)
               .FirstOrDefaultAsync(q => q.Id == allocationId);

        var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

        return model;
    }

    public async Task EditAllocation(LeaveAllocationEditVM allocationEditVm)
    {
        //var leaveAllocation = await GetEmployeeAllocation(allocationEditVm.Id);
        //if (leaveAllocation == null)
        //{
        //    throw new Exception("Leave allocation record doesn't exist.");
        //}
        //leaveAllocation.NumberOfDays = allocationEditVm.NumberOfDays;
        // option 1 _context. Update (leave Allocation);
        //option2 _context.Entry(leaveAllocation).State = EntityState.Modified;
        //_context.SaveChanges();

        await _context.LeaveAllocations
           .Where(q => q.Id == allocationEditVm.Id)
           .ExecuteUpdateAsync(s => s.SetProperty(e => e.NumberOfDays, allocationEditVm.NumberOfDays));
    }

    private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
    {
        //string employeeId = string.Empty;
        //if (!string.IsNullOrEmpty(userId))
        //{
        //    employeeId = userId;
        //}
        //else
        //{
        //    var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        //    employeeId = user.Id;
        //}

        var currentDate = DateTime.Now;

        //var period = await _context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
        //var leaveAllocation = await _context.LeaveAllocations
        //                        .Include(l => l.LeaveType)
        //                        .Include(l => l.Period)
        //                        .Where(q => q.EmployeeId == user.Id && q.PeriodId == period.Id)
        //                        .ToListAsync();

        var leaveAllocation = await _context.LeaveAllocations
                                .Include(l => l.LeaveType)
                                .Include(l => l.Period)
                                .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year == currentDate.Year)
                                .ToListAsync();
        return leaveAllocation;
    }

    private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
    {
        var exists = await _context.LeaveAllocations.AnyAsync(q =>
        q.EmployeeId == userId
        && q.LeaveTypeId == leaveTypeId
        && q.PeriodId == periodId);

        return exists;
    }

}
