namespace PokeBattler.Common.Models.Interfaces
{
    public interface IFactory<T> where T : class, new()
    {
        public T Create();
    }
}
