using Microsoft.EntityFrameworkCore;
namespace CISProject.EFCore.Infrastructure
{
    public class CISProjectDataContext: DbContext
    {
        public CISProjectDataContext(DbContextOptions<CISProjectDataContext> options) : base(options)
        {

        }
    }
}