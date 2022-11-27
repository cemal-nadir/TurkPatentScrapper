using System.Threading.Tasks;
using TPHunter.Source.Main.Workers;

namespace TPHunter.Source.Main
{
    internal static class Program
    {
        private static readonly MainWorker Worker = new ();

        private static async Task Main()
        {
            Startup.SetConfigs();
           
            await Worker.DoWork().ConfigureAwait(false);
        }
    }
}
