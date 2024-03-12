using MyDeckStats.Domain.Models;

namespace MyDeckStats.Domain.Interfaces.Services.Scryfall
{
    public interface IScryfallMtgSetService<T> 
        where T : class
    {
        Task<IEnumerable<T>?> GetAll();

        Task<T?> GetByScryfallId(string id);
    }
}
