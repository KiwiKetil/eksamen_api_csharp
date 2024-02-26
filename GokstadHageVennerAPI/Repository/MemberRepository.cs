using GokstadHageVennerAPI.Data;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GokstadHageVennerAPI.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly GokstadHageVennerDbContext _dbContext;
    private readonly ILogger<MemberRepository> _logger;

    public MemberRepository(GokstadHageVennerDbContext dbContext, ILogger<MemberRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ICollection<Member>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting members from db");

        int itemsToSkip = (page - 1) * pageSize;

        return await _dbContext.Members
            .OrderBy(u => u.Id)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        _logger.LogDebug("Getting member: {id} from db", id);

        return await _dbContext.Members.FindAsync(id);
    }

    public async Task<Member?> GetByNameAsync(string userName)
    {
        _logger.LogDebug("Getting member by username: {username} from db", userName);

        var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
        return member;
    }

    public async Task<Member?> AddAsync(Member entity)
    {
        _logger.LogDebug("Adding member to db");

        await _dbContext.Members.AddAsync(entity);
        await _dbContext.SaveChangesAsync();    
        return entity;
    }

    public async Task<Member?> UpdateAsync(int id, Member member)
    {
        _logger?.LogDebug("Updating member id: {id} in db", id);

        var mbr = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == id);
        if (mbr == null)
            return null;

        mbr.UserName = string.IsNullOrEmpty(member.UserName) ? mbr.UserName : member.UserName;
        mbr.FirstName = string.IsNullOrEmpty(member.FirstName) ? mbr.FirstName : member.FirstName;
        mbr.LastName = string.IsNullOrEmpty(member.LastName) ? mbr.LastName : member.LastName;
        mbr.Email = string.IsNullOrEmpty(member.Email) ? mbr.Email : member.Email;
        mbr.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return mbr;
    }

    public async Task<Member?> DeleteByIdAsync(int id)
    {
        _logger?.LogDebug("Deleting member id: {id} from db", id);

        var res = await _dbContext.Members.FindAsync(id);
        if (res != null)
        {
            _dbContext.Members.Remove(res);
            await _dbContext.SaveChangesAsync();
            return res;
        }
        return null;
    }
}
