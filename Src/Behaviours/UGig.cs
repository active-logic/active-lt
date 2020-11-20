// Doc/Reference/UGig.md
#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

using System;
using Active.Core.Details;
using static Active.Status;

namespace Active.Core{
public abstract partial class UGig : UnityEngine.MonoBehaviour {

    protected static readonly LogString log = null;

    public virtual status Step()
    => fail(log && "`Step` is not implemented");

    public action Pause(float duration) => stepper.Pause(duration);

    public action Resume() => stepper.Resume();

    public action Suspend() => stepper.Suspend();

    protected action Do(params object[] x) => @void();

    // PROVISIONAL
    protected status Do<T>() where T: UGig{
        var c = GetComponent<T>();
        if(c == null) c = gameObject.AddComponent<T>();
        return c;
    }

    public bool suspended => !stepper.enabled;

    public static implicit operator Func<status> (UGig self)
    => self.Step;

    public static implicit operator status (UGig self)
    => self.Step();

    Stepper stepper => GetComponentInParent<Stepper>();

}}
