using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IVillaNumberRepository VillaNumberRepository { get; }

        public IVillaRepository VillaRepository { get; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db,IVillaNumberRepository villaNumber,IVillaRepository villa)
        {
            VillaNumberRepository = villaNumber;
            VillaRepository = villa;
            _db = db;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}
