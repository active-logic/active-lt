// Doc/Reference/Steppers.md
#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

using static Active.Status;

namespace Active.Core{
[UnityEngine.AddComponentMenu("Active Logic/Agent")]
public class Agent : Stepper{

    public status state;

    void Update() => state = Step();

    override public void Run(System.Func<status> φ){
        base.Run(φ);
        // NOTE: Organic status right after `Run` is called may be
        // other than `cont`, which breaks functional tests, esp when
        // agent is created in a setup fixture.
        state = cont();
    }

    #if !AL_BEST_PERF

    // TODO: provisional - when would this be used?
    public T Do<T>() where T: UGig{
        var x = gameObject.AddComponent<T>();
        root = x;
        enabled = true;
        return x;
    }

    #endif

}}
