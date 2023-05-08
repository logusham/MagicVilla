namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IVillaNumberRepository VillaNumberRepository { get; }
        IVillaRepository VillaRepository { get; }
        void Save();
    }
}
