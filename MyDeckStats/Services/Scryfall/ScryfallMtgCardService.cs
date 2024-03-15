using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;
using Newtonsoft.Json;

namespace MyDeckStats.Services.Scryfall
{
    public class ScryfallMtgCardService : IScryfallMtgCardService<ScryfallMtgCard>
    {
        protected readonly ScryfallApiServerClient ScryfallClient;
        protected readonly IMtgCardService MtgCardService;

        public ScryfallMtgCardService(ScryfallApiServerClient client, IMtgCardService mtgCardService)
        {
            ScryfallClient = client;
            MtgCardService = mtgCardService;
        }

        public Task<bool> ImportFromFile()
        {
            string FilePath = "C:\\Users\\ftrue\\source\\repos\\MyDeckStats\\MyDeckStats\\BulkCards\\BulkTest.json";

            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException("File not found", FilePath);
            }

            try
            {
                var cards = JsonConvert.DeserializeObject<List<ScryfallMtgCard>>(File.ReadAllText(FilePath));

                cards!.ForEach(delegate(ScryfallMtgCard card)
                {
                    var ExistingCard = MtgCardService.Filter(x => x.OracleId == new Guid(card.oracle_id!)).FirstOrDefault();

                    if (ExistingCard == null)
                    {
                        MtgCardService.Add(card.ScryfallTransform());
                    }
                    else
                    {
                        var GetCard = MtgCardService.GetById(new Guid(card.Id!));
                        MtgCardService.Update(GetCard!.MapScryFallOnto(card));
                    }
                    
                });

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex}", ex);
            }
        }
    }
}
