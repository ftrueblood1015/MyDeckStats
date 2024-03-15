using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Models;
using MyDeckStats.Services.Scryfall;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyDeckStats
{
    public static class Extensions
    {
        public static MtgSet ScryfallTransform(this ScryfallMtgSet scryFallMtgSet)
        {
            return new MtgSet()
            {
                Id = scryFallMtgSet.Id,
                Code = scryFallMtgSet.Code,
                Name = scryFallMtgSet.Name,
                Uri = scryFallMtgSet.Uri,
                ScryfallUri = scryFallMtgSet.ScryfallUri,
                SearchUri = scryFallMtgSet.SearchUri,
                ReleasedAt = scryFallMtgSet.ReleasedAt,
                SetType = scryFallMtgSet.SetType,
                CardCount = scryFallMtgSet.CardCount,
                ParentSetCode = scryFallMtgSet?.ParentSetCode,
                Digital = scryFallMtgSet!.Digital,
                NonfoilOnly = scryFallMtgSet.NonfoilOnly,
                FoilOnly = scryFallMtgSet.FoilOnly,
                IconSvgUri = scryFallMtgSet.IconSvgUri
            };
        }

        public static MtgSet MapScryFallOnto(this MtgSet mtgSet, ScryfallMtgSet scryFallMtgSet)
        {
            mtgSet.Id = scryFallMtgSet.Id;
            mtgSet.Code = scryFallMtgSet.Code;
            mtgSet.Name = scryFallMtgSet.Name;
            mtgSet.Uri = scryFallMtgSet.Uri;
            mtgSet.ScryfallUri = scryFallMtgSet.ScryfallUri;
            mtgSet.SearchUri = scryFallMtgSet.SearchUri;
            mtgSet.ReleasedAt = scryFallMtgSet.ReleasedAt;
            mtgSet.SetType = scryFallMtgSet.SetType;
            mtgSet.CardCount = scryFallMtgSet.CardCount;
            mtgSet.ParentSetCode = scryFallMtgSet?.ParentSetCode;
            mtgSet.Digital = scryFallMtgSet!.Digital;
            mtgSet.NonfoilOnly = scryFallMtgSet.NonfoilOnly;
            mtgSet.FoilOnly = scryFallMtgSet.FoilOnly;
            mtgSet.IconSvgUri = scryFallMtgSet.IconSvgUri;

            return mtgSet;
        }

        public static MtgCard ScryfallTransform(this ScryfallMtgCard scryfallMtgCard)
        {
            scryfallMtgCard.VerifyNotNull();

            return new MtgCard()
            {
                Id = new Guid(scryfallMtgCard.Id!),
                Name = scryfallMtgCard.Name,
                Description = scryfallMtgCard.Name,
                OracleId = new Guid(scryfallMtgCard.oracle_id!),
                ScryfallUri = scryfallMtgCard.scryfall_uri,
                ColorIdentity = String.Join(",", scryfallMtgCard.color_identity!),
                manaCost = scryfallMtgCard.mana_cost,
                ConvertedManaCost = (int)Math.Round(scryfallMtgCard.cmc),
                Type = scryfallMtgCard.type_line,
                OracleText = scryfallMtgCard.oracle_text,
                Power = int.TryParse(scryfallMtgCard.power, out int x) ? x : 0,
                Toughness = int.TryParse(scryfallMtgCard.toughness, out int y) ? y : 0,
                Rarity = scryfallMtgCard.rarity,
                EdhrecRank = scryfallMtgCard.edhrec_rank,
                PennyRank = scryfallMtgCard.penny_rank,
                ProducesMana = scryfallMtgCard.produced_mana != null ? true : false,
                Slug = scryfallMtgCard.Name!.ToUpper()
            };
        }

        public static MtgCard MapScryFallOnto(this MtgCard mtgCard, ScryfallMtgCard scryfallMtgCard)
        {
            mtgCard.Name = scryfallMtgCard.Name;
            mtgCard.Description = scryfallMtgCard.Name;
            mtgCard.ScryfallUri = scryfallMtgCard.scryfall_uri;
            mtgCard.ColorIdentity = String.Join(",", scryfallMtgCard.color_identity!);
            mtgCard.manaCost = scryfallMtgCard.mana_cost;
            mtgCard.ConvertedManaCost = (int)Math.Round(scryfallMtgCard.cmc);
            mtgCard.Type = scryfallMtgCard.type_line;
            mtgCard.OracleText = scryfallMtgCard.oracle_text;
            mtgCard.Power = int.TryParse(scryfallMtgCard.power, out int x) ? x : 0;
            mtgCard.Toughness = int.TryParse(scryfallMtgCard.toughness, out int y) ? y : 0;
            mtgCard.Rarity = scryfallMtgCard.rarity;
            mtgCard.EdhrecRank = scryfallMtgCard.edhrec_rank;
            mtgCard.ProducesMana = scryfallMtgCard.produced_mana != null ? true : false;

            return mtgCard;
        }

        public static async Task<T> GetData<T>(this ScryfallApiServerClient client, Uri uri)
        {
            uri = uri.VerifyNotNull();
            client = client.VerifyNotNull();

            string rawData = await client.Client.GetStringAsync(uri);

            var data = JsonConvert.DeserializeObject<T>(rawData);

            if (data == null)
            {
                return Activator.CreateInstance<T>();
            }

            return data;
        }

        public static async Task<IEnumerable<ScryfallMtgSet>> GetScryfallSetData<T>(this ScryfallApiServerClient client, Uri uri)
        {
            uri = uri.VerifyNotNull();
            client = client.VerifyNotNull();

            string jsonString  = await client.Client.GetStringAsync(uri);

            JObject jsonObject = JObject.Parse(jsonString);

            JArray dataArray = (JArray)jsonObject["data"]!;

            if (dataArray == null)
            {
                return Activator.CreateInstance<IEnumerable<ScryfallMtgSet>>();
            }

            var results = new List<ScryfallMtgSet>();

            foreach (JToken token in dataArray)
            {
                var newSet = new ScryfallMtgSet()
                {
                    Object = token["object"]?.ToString(),
                    Id = new Guid(token["id"]?.ToString()!),
                    Code = token["code"]?.ToString(),
                    Name = token["name"]?.ToString(),
                    Uri = token["uri"]?.ToString(),
                    ScryfallUri = token["scryfall_uri"]?.ToString(),
                    SearchUri = token["search_uri"]?.ToString(),
                    SetType = token["set_type"]?.ToString(),
                    CardCount = int.TryParse(token["card_count"]?.ToString(), out int x) ? x : 0,
                    ParentSetCode = token["parent_set_code"]?.ToString(),
                    Digital = token["digital"]?.ToString() == "true" ? true : false,
                    NonfoilOnly = token["nonfoil_only"]?.ToString() == "true" ? true : false,
                    FoilOnly = token["foil_only"]?.ToString() == "true" ? true : false,
                    IconSvgUri = token["icon_svg_uri"]?.ToString(),
                    ReleasedAt = DateTime.Parse(token["released_at"]?.ToString()!)
                };
                results.Add(newSet);
            }

            return results;
        }

        public static T VerifyNotNull<T>(this T? value)
        where T : class
        {
            switch (value)
            {
                case string strVal:
                    if (string.IsNullOrWhiteSpace(strVal))
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    return value;

                default:
                    return value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public static bool CompareToScryfall(this MtgCard mtgCard, ScryfallMtgCard scryfallMtgCard)
        {
            return (
                mtgCard.Description == scryfallMtgCard.Name &&
                mtgCard.Description == scryfallMtgCard.Name &&
                mtgCard.ScryfallUri == scryfallMtgCard.scryfall_uri &&
                mtgCard.ColorIdentity == String.Join(",", scryfallMtgCard.color_identity!) &&
                mtgCard.manaCost == scryfallMtgCard.mana_cost &&
                mtgCard.ConvertedManaCost == (int)Math.Round(scryfallMtgCard.cmc) &&
                mtgCard.Type == scryfallMtgCard.type_line &&
                mtgCard.OracleText == scryfallMtgCard.oracle_text &&
                mtgCard.Power == (int.TryParse(scryfallMtgCard.power, out int x) ? x : 0) &&
                mtgCard.Toughness == (int.TryParse(scryfallMtgCard.toughness, out int y) ? y : 0) &&
                mtgCard.Rarity == scryfallMtgCard.rarity &&
                mtgCard.EdhrecRank == scryfallMtgCard.edhrec_rank &&
                mtgCard.ProducesMana == (scryfallMtgCard.produced_mana != null ? true : false)
            );
        }
    }
}
