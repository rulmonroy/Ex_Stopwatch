using System;

namespace Ex_Stopwatch
{
	public class Stopwatch
	{
		public Stopwatch()
		{
			Reset();
		}

		#region Properties

		public DateTime StartTime { get; private set; }
		public DateTime EndTime { get; private set; }

		TimeSpan _totalElapsed;
		public TimeSpan Elapsed
		{
			get
			{
				var lapse = _totalElapsed;
				if (IsStarted && IsRunning && EndTime >= StartTime)
				{
					lapse += (EndTime - StartTime);
				}
				return lapse;
			}
		}

		public bool IsRunning { get; private set; }
		public bool IsStarted
		{
			get => StartTime > DateTime.MinValue;
			private set { }
		}

		#endregion

		public void Start()
		{
			if (IsRunning)
			{
				throw new InvalidOperationException("Stopwatch is already running.");
			}
			IsStarted = true;
			IsRunning = true;
			StartTime = DateTime.UtcNow;
		}

		public void Stop()
		{
			if (IsRunning)
			{
				EndTime = DateTime.UtcNow;
				_totalElapsed += (EndTime - StartTime);
				IsRunning = false;
			}
		}

		public void Reset()
		{
			_totalElapsed = TimeSpan.Zero;
			StartTime = DateTime.MinValue;
			EndTime = DateTime.MinValue;
			IsRunning = false;
		}
	}
}
