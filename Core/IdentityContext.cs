using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class IdentityContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
}