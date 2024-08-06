using System.Threading.Tasks;

using Foundation.Extension.Fixtures;
using XXXXX.Context.Migrations.Shared;


namespace XXXXX.Context.Migrations
{
    class Program
    {
        static async Task Main(string[] args)
        {            
            FixtureHelper.FIXTURES_DIRECTORY = "Fixtures-PRD";

            await BaseProgram.Main(args, typeof(Program).Assembly.GetName().Name);
        }
    }
}
