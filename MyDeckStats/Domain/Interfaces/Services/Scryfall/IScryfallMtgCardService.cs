namespace MyDeckStats.Domain.Interfaces.Services.Scryfall
{
    public interface IScryfallMtgCardService<T>
        where T : class
    {
        Task<bool> ImportFromFile();
    }
}
