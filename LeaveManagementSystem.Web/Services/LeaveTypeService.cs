using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services;

public class LeaveTypeService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypeService
{
    //since we are using the new C# 12 feature of primary constructors by adding the parameters to the class declaration
    //public LeaveTypeSerice(ApplicationDbContext context, IMapper mapper)
    //{
    //    this.context = context;
    //    this.mapper = mapper;
    //}

    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        var data = await _context.LeaveTypes.ToListAsync();
        var leaveTypesVM = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
        return leaveTypesVM;
    }

    public async Task<T?> Get<T>(int? id) where T : class
    {
        if (id == null)
        {
            return null;
        }
        var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
        if (leaveType == null)
        {
            return null;
        }
        var leaveTypeVM = _mapper.Map<T>(leaveType);
        return leaveTypeVM;
    }

    public async Task Remove(int id)
    {
        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leaveType != null)
        {
            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Edit(LeaveTypeEditVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Update(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task Create(LeaveTypeCreateVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Add(leaveType);
        await _context.SaveChangesAsync();
    }


    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName));
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        var lowercaseName = leaveTypeEdit.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName) &&
                                                    q.Id != leaveTypeEdit.Id);
    }
}
