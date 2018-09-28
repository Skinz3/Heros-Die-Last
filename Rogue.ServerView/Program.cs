using System;
using System.Threading;

namespace Rogue.ServerView
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Server.Program.OnInitialized += Program_OnInitialized;
            Server.Program.Run();
        }

        private static void Program_OnInitialized()
        {
            var thread = new Thread(new ThreadStart(new Action(() =>
              {
                  using (var game = new Game1())
                      game.Run();
              })));

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
