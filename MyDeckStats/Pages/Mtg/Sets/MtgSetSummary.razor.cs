using Microsoft.AspNetCore.Components;
using MyDeckStats.Commands.Navigation;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;
using MyDeckStats.Pages.Shared;

namespace MyDeckStats.Pages.Mtg.Sets
{
    public partial class MtgSetSummary : SummaryPageBase<MtgSet>
    {
        [Inject]
        private IScryfallMtgSetService<ScryfallMtgSet> ScryfallMtgSetService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string DetailRoute = "mtgsetdetail";

        public override Func<MtgSet, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private async Task UpdateScryfallAll()
        {
            if (ScryfallMtgSetService == null)
            {
                throw new Exception($"{nameof(ScryfallMtgSetService)}  is null!");
            }

            var response = await ScryfallMtgSetService.GetAll();

            foreach(var data in response)
            {
                UpdateOrCreate(data);
            }
        }

        private void UpdateOrCreate(ScryfallMtgSet set)
        {
            if (Service == null)
            {
                throw new Exception($"{nameof(Service)}  is null!");
            }

            var response = Service.GetById(set.Id);

            if (response == null)
            {
                Service.Add(set.ScryfallTransform());
            } 
            else
            {
                var setToUpdate = Service.GetById(response.Id);

                setToUpdate!.MapScryFallOnto(set);

                Service.Update(setToUpdate!);
            }
        }

        public void View(MtgSet set, string route)
        {
            var navCommand = new NavigationCommand(NavigationManager!, $"/{route}/{set.Id}", false);
            navCommand.Execute();
        }
    }
}
