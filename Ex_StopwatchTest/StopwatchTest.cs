using Ex_Stopwatch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;

namespace Ex_StopwatchTest
{
	[TestClass]
	public class StopwatchTest
	{
		private Stopwatch _stopwatch;

		[SetUp]
		public void SetUp()
		{
			_stopwatch = new Stopwatch();
		}

		[Test]
		public void TestInitialState()
		{
			Assert.That(_stopwatch.StartTime, Is.EqualTo(DateTime.MinValue));
			Assert.That(_stopwatch.EndTime, Is.EqualTo(DateTime.MinValue));
			Assert.That(_stopwatch.Elapsed, Is.EqualTo(TimeSpan.Zero));
			Assert.That(_stopwatch.IsRunning, Is.False);
			Assert.That(_stopwatch.IsStarted, Is.False);
		}

		#region TestStart

		[Test]
		public void TestStart_ShouldSetupData()
		{
			_stopwatch.Start();
			Assert.That(_stopwatch.Elapsed, Is.EqualTo(TimeSpan.Zero));
			Assert.That(_stopwatch.IsRunning, Is.True);
			Assert.That(_stopwatch.IsStarted, Is.True);
		}

		[Test]
		public void TestStart_AttemptTwice_ShouldFail()
		{
			_stopwatch.Start();
			Assert.Throws(typeof(InvalidOperationException), _stopwatch.Start);
		}

		#endregion

		#region TestStop

		[Test]
		public void TestStop_Once_ShouldChangeStatus()
		{
			_stopwatch.Start();
			_stopwatch.Stop();
			Assert.That(_stopwatch.IsRunning, Is.False);
			Assert.That(_stopwatch.IsStarted, Is.True);
		}

		[Test]
		public void TestStop_BeforeStarted_ShouldDoNothing()
		{
			_stopwatch.Stop();
			Assert.That(_stopwatch.Elapsed, Is.EqualTo(TimeSpan.Zero));
			Assert.That(_stopwatch.IsRunning, Is.False);
			Assert.That(_stopwatch.IsStarted, Is.False);
		}

		[Test]
		public void TestStop_Twice_ShouldNotChangeStatus()
		{
			_stopwatch.Start();
			_stopwatch.Stop();
			_stopwatch.Stop();
			Assert.That(_stopwatch.IsRunning, Is.False);
			Assert.That(_stopwatch.IsStarted, Is.True);
		}

		#endregion

		#region TestReset

		[Test]
		public void TestReset_BeforeStart_ShouldDoNothing()
		{
			_stopwatch.Reset();
			Assert.That(_stopwatch.Elapsed, Is.EqualTo(TimeSpan.Zero));
			Assert.That(_stopwatch.IsRunning, Is.False);
			Assert.That(_stopwatch.IsStarted, Is.False);
		}

		[Test]
		public void TestReset_WhileRunning_ShouldResetStatus()
		{
			_stopwatch.Start();
			_stopwatch.Reset();
			Assert.That(_stopwatch.Elapsed, Is.EqualTo(TimeSpan.Zero));
			Assert.That(_stopwatch.IsRunning, Is.False);
			Assert.That(_stopwatch.IsStarted, Is.False);
		}

		[Test]
		public void TestReset_WhilePaused_ShouldResetStatus()
		{
			_stopwatch.Start();
			_stopwatch.Stop();
			_stopwatch.Reset();
			Assert.That(_stopwatch.Elapsed, Is.EqualTo(TimeSpan.Zero));
			Assert.That(_stopwatch.IsRunning, Is.False);
			Assert.That(_stopwatch.IsStarted, Is.False);
		}

		#endregion

		#region TestElapsed

		[Test]
		public void TestElapsedCalculations()
		{
			_stopwatch.Start();

			// Wait for 3 seconds and stop
			var startTime1 = _stopwatch.StartTime;
			var endTime1 = startTime1.AddSeconds(3);
			while (DateTime.UtcNow <= endTime1) { }
			_stopwatch.Stop();
			Assert.That(_stopwatch.Elapsed.Seconds, Is.EqualTo(3));

			// Wait for 2 seconds and start
			var startTime2 = endTime1.AddSeconds(2);
			while (DateTime.UtcNow <= startTime2) { }
			_stopwatch.Start();
			Assert.That(_stopwatch.StartTime.Minute, Is.EqualTo(startTime2.Minute));
			Assert.That(_stopwatch.StartTime.Second, Is.EqualTo(startTime2.Second));

			// Wait for 2 more seconds and stop again
			var endTime2 = startTime2.AddSeconds(2);
			while (DateTime.UtcNow <= endTime2) { }
			_stopwatch.Stop();
			Assert.That(_stopwatch.Elapsed.Seconds, Is.EqualTo(5));
		}

		#endregion
	}
}
