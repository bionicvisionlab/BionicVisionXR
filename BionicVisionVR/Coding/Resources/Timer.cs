using System;
using System.Diagnostics;


public class Timer
{
    private Stopwatch watch;
    private string titleText;

    public Timer()
    {
        watch = new Stopwatch();
        titleText = "Run Time";
    }

    public Timer(String text)
    {
        watch = new Stopwatch();
        titleText = text;
    }
    public void start()
    {
        watch.Start();
    }

    public void stopAndLog()
    {
        watch.Stop();
        TimeSpan ts = watch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        UnityEngine.Debug.Log(titleText + ": " + elapsedTime);
    }
}
