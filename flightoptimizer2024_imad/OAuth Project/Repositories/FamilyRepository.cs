using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;

namespace OAuth_Project.Repositories
{
    public class FamilyRepository:IFamilyRepository
    {

        private readonly MyAppContext _context;
        public FamilyRepository(MyAppContext context)
        {
            _context = context;
        }

        public async Task<Family> InsertFamily(Family family)
        {
            if (family == null) return null;
            _context.Families.Add(family);
            _context.SaveChanges();
            return family;
        }
    }
}
