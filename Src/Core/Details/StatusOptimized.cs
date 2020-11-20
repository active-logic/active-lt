#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif
#if AL_OPTIMIZE

using Active.Core.Details;
using static System.Math;
using ArgEx = System.ArgumentException;

namespace Active.Core{
public readonly partial struct status{

    internal static readonly status _done = @unchecked(+1),
                                    _fail = @unchecked(-1),
                                    _cont = @unchecked( 0);

    public static bool log = true;

    public bool failing  => ω <= -1;
    public bool running  => ω ==  0;
    public bool complete => ω >=  1;
    internal int  raw      => ω;

    public status this[ValidString reason] => this;

    public status Map(in status failTo, in status contTo,
                                        in status doneTo){
        switch(ω){
            case -1: return new status(this, failTo.ω);
            case  0: return new status(this, contTo.ω);
            case +1: return new status(this, doneTo.ω);
            default: throw new ArgEx();
        }
    }

    // NOTE: while supported, the conditional logical operators `&&` and `||`
	// are not explicitly implemented.

    public static status operator + (in status x, in status y)
    => new status(Max(x.ω, y.ω));

    public static status operator * (in status x, in status y)
    => new status(Min(x.ω, y.ω));

    public static status operator % (in status x, in status y)
    => new status(x.ω);

    public static status  operator % (status x, ValidString reason)
    => x;

    public static status  operator ! (in status s)
    => new status(-s.ω);

    public static status operator ~ (in status s)
    => new status(s.ω * s.ω);

    public static status operator + (in status s)
    => new status(System.Math.Min(s.ω + 1, +1));

    public static status operator - (in status s)
    => new status(System.Math.Max(s.ω - 1, -1));

}}

#endif
