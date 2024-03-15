using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;

namespace MyDeckStats.Services.Scryfall
{
    public class ScryfallMtgSetService : IScryfallMtgSetService<ScryfallMtgSet>
    {
        protected readonly ScryfallApiServerClient ScryfallClient;

        public ScryfallMtgSetService(ScryfallApiServerClient client)
        {
            ScryfallClient = client;
        }

        public async Task<IEnumerable<ScryfallMtgSet>?> GetAll()
        {
            var url = new Uri(SiteConstants.ScryfallSetUrl, UriKind.Relative);
            var result = await ScryfallClient.GetScryfallSetData<IEnumerable<ScryfallMtgSet>>(url);
            return result;
        }

        public async Task<ScryfallMtgSet?> GetByScryfallId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
