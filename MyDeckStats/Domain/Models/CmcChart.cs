using MudBlazor;

namespace MyDeckStats.Domain.Models
{
    public class CmcChart
    {
        public List<ChartSeries>? Series { get; set; }
        public string[]? XAxisLabels {  get; set; }
        public ChartOptions? Options { get; set; }
        public ChartType ChartType { get; set; }
    }
}
