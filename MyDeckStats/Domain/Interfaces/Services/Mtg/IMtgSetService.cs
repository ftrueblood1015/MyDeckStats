﻿using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.Domain.Interfaces.Services.Mtg
{
    public interface IMtgSetService : IServiceBase<MtgSet>
    {
        MtgSet? GetByScryfallId(string id);
    }
}
