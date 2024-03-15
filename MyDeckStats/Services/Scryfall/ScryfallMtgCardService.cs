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

        public async Task<bool> DownloadFileAndImport()
        {
            var downloadData = await GetDownloadUri();

            try
            {
                using (HttpResponseMessage response = await ScryfallClient.Client.GetAsync(downloadData.download_uri))
                using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream fileStream = File.Create(SiteConstants.ScryfallDownloadFile))
                    {
                        await contentStream.CopyToAsync(fileStream);
                    }
                }

                var result = await Import(SiteConstants.ScryfallDownloadFile);

                if (result)
                {
                    File.Delete(SiteConstants.ScryfallDownloadFile);
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
            string FilePath = SiteConstants.ScryfallTestFile;

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
                    var Exists = MtgCardService.Filter(x => x.OracleId == new Guid(card.oracle_id!)).Count();

                    if (Exists == 0)
                    {
                        MtgCardService.Add(card.ScryfallTransform());
                    }
                    else
                    {
                        var ExistingCard = MtgCardService.GetById(new Guid(card.Id!));
                        if (!ExistingCard!.CompareToScryfall(card))
                        {
                            MtgCardService.Update(ExistingCard!.MapScryFallOnto(card));
                        }
                    }

                });

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex}", ex);
            }
        }

        public async Task<OracleCardsDownload> GetDownloadUri()
        {
            var url = new Uri(SiteConstants.ScryfallBulkDownLoadUrl, UriKind.Relative);
            var downloadData = await ScryfallClient.GetData<OracleCardsDownload>(url);
            return downloadData;
        }
    }
}
