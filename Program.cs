using System;

namespace Ex_Stopwatch
{
	class Program
	{
		static void Main(string[] args)
		{
			var stopwatch = new Stopwatch();
			var pressedKey = ConsoleKey.Enter;

			while (!Console.KeyAvailable && pressedKey != ConsoleKey.Escape)
			{
				DisplayStatus(stopwatch);

				pressedKey = Console.ReadKey(true).Key;
				if (pressedKey == ConsoleKey.Spacebar)
				{
					if (stopwatch.IsRunning)
					{
						stopwatch.Stop();
					}
					else
					{
						stopwatch.Start();
					}
				}
				else if (pressedKey == ConsoleKey.Enter)
				{
					stopwatch.Reset();
				}
			}
		}

		static void DisplayStatus(Stopwatch stopwatch)
		{
			Console.Clear();
			Console.WriteLine("Press [Spacebar] to start/pause the stopwatch.");
			Console.WriteLine("Press [Enter] to reset the stopwatch.");
			Console.WriteLine("Press [Esc] to exit.\n\n");

			if (stopwatch.IsStarted)
			{
				Console.WriteLine($"Started: {stopwatch.StartTime}");
				Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
			}
			var status = stopwatch.IsRunning ? "Running..." : stopwatch.IsStarted ? "Paused..." : "Ready!";
			Console.WriteLine($"\nStatus: {status}");

			if (stopwatch.IsStarted && !stopwatch.IsRunning)
			{
				Console.WriteLine($"Paused on: {stopwatch.EndTime}");
			}
		}
	}
}
