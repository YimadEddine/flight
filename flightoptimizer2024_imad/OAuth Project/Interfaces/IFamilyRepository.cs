using OAuth_Project.Models;

namespace OAuth_Project.Interfaces
{
    public interface IFamilyRepository
    {
        Task<Family> InsertFamily(Family family);
    }
}
