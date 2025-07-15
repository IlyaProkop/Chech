namespace Game.Runtime.Infrastructure.Repository
{
    public interface IRepositoryService
    {
        public void Save<TData>(TData data, string saveKey = null);
        public bool TryLoad<TData>(out TData loadedData, string saveKey = null);
        public bool HasData<TData>(string saveKey = null);
        public void Delete<TData>(string saveKey = null);
    }
}
