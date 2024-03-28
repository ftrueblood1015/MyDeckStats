namespace MyDeckStats.Domain.Interfaces.Services.Mtg
{
    public interface ICardProcessingService
    {
        bool ProcessCardKeywords();
        bool ProcessCardColorIdentities();
        bool ProcessCardTypes();
        bool ProcessCardPurpose(Guid CardId);
    }
}
