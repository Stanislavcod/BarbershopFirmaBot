
HumanTimeFormat.formatDuration(8684256);
public class HumanTimeFormat
{
    public static void formatDuration(int seconds)
    {
        var ts = TimeSpan.FromSeconds(seconds);
        if (seconds == 0) Console.WriteLine("now");
        Console.WriteLine("{0} д. {1} ч. {2} м. {3} с. {4} мс.", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
    }
}
