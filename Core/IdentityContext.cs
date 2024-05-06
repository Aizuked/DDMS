using Core.Models.Identitiy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<User>(options);