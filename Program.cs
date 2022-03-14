using System;

namespace DontOpenTheChest
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DontOpenTheChest())
                game.Run();
        }
    }
}
