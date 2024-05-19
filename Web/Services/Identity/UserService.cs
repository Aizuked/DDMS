using Core;
using Core.Models.Identity;

namespace Web.Services.Identity;

public class UserService
{
    private readonly IdentityContext _context;

    public UserService(
        IdentityContext context
    )
    {
        _context = context;
    }

    public IQueryable<User> GetQuery() =>
        _context
            .Users;

    public IQueryable<User> ById(int id) =>
        GetQuery()
            .Where(i => i.Id == id);
}