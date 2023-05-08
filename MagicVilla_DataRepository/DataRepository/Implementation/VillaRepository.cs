using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedData = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return  entity;
        }
    }
}
