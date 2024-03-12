﻿using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class MtgSetRepository : RepositoryBase<MtgSet, ApplicationDbContext>, IMtgSetRepository
    {
        public MtgSetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public MtgSet? GetByScryfallId(string id)
        {
            return Context.Set<MtgSet>().FirstOrDefault(x => x.ScryfallId == id);
        }
    }
}
