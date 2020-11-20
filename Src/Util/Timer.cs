using System;
using System.Diagnostics;

public static class Timer{

    public static int Profile(Action x){
        var w = Stopwatch.StartNew();
        x();
        return (int)w.Elapsed.TotalMilliseconds;
    }

}
