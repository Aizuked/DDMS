using Microsoft.EntityFrameworkCore;

namespace Core;

public class DdmsDbContext(DbContextOptions<DdmsDbContext> options) : DbContext(options)
{
    
}