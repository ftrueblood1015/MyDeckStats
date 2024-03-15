using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;

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

        public async Task<bool> DownloadFileAndImport()
        {
            var url = new Uri("bulk-data/oracle-cards", UriKind.Relative);
            var downloadData = await ScryfallClient.GetData<OracleCardsDownload>(url);

            try
            {
                using (HttpResponseMessage response = await ScryfallClient.Client.GetAsync(downloadData.download_uri))
                using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream fileStream = File.Create("oracle_cards.json"))
                    {
                        await contentStream.CopyToAsync(fileStream);
                    }
                }

                var result = await Import("oracle_cards.json");

                if (result)
                {
                    File.Delete("oracle_cards.json");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading Oracle cards: {ex.Message}");
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> ImportFromFile()
        {
            string FilePath = "C:\\Users\\ftrue\\source\\repos\\MyDeckStats\\MyDeckStats\\BulkCards\\BulkTest.json";

            return await Import(FilePath);
        }

        public Task<bool> Import(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            try
            {
                var cards = JsonConvert.DeserializeObject<List<ScryfallMtgCard>>(File.ReadAllText(filePath));

                cards!.ForEach(delegate (ScryfallMtgCard card)
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
