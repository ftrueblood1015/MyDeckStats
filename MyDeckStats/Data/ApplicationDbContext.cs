using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Entities.Mtg.Decks;
using static MudBlazor.CategoryTypes;

namespace MyDeckStats.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        DbSet<MtgSet> MtgSets => Set<MtgSet>();
        DbSet<MtgCard> MtgCards => Set<MtgCard>();
        DbSet<CardType> CardTypes => Set<CardType>();
        DbSet<ColorIdentity> ColorIdentities => Set<ColorIdentity>();
        DbSet<MtgKeyword> MtgKeywords => Set<MtgKeyword>();
        DbSet<SetCard> SetCards => Set<SetCard>();
        DbSet<MasterType> MasterTypes => Set<MasterType>();
        DbSet<MasterPurpose> MasterPurposes => Set<MasterPurpose>();
        DbSet<CardPurpose> CardPurposes => Set<CardPurpose>();
        DbSet<Format> Formats => Set<Format>();
        DbSet<Guild> Guilds => Set<Guild>();
        DbSet<Deck> Decks => Set<Deck>();
        DbSet<DeckCard> DeckCards => Set<DeckCard>();
    }
}
