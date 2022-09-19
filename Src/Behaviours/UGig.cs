// Doc/Reference/UGig.md
#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

using System; using Ex = System.Exception;
using UnityEngine;
using Active.Core.Details;
using static Active.Status;

namespace Active.Core{
public abstract partial class UGig : MonoBehaviour {

    protected static readonly LogString log = null;

    virtual public bool rootable => true;

    public virtual status Step()
    => fail(log && "`Step` is not implemented");

    public loop Pause(float duration) => stepper.Pause(duration);

    public loop Push(Func<status> φ) => stepper.Push(φ);

    public action Resume() => stepper.Resume();

    public action Suspend() => stepper.Suspend();

    public bool suspended => !stepper.enabled;

    public loop this[Func<status> φ] => Push(φ);

    public static implicit operator Func<status> (UGig self)
    => self.Step;

    public static implicit operator status (UGig self)
    => self.Step();

    // --------------------------------------------------------------

    protected action Do(params object[] x) => @void();

    protected status Do<T>() where T: UGig{
        var c = GetComponent<T>();
        if(c == null) c = gameObject.AddComponent<T>();
        return c;
    }

    protected action DrawLine(Vector3 end, float duration = 0.0f,
                              bool depthTest = true)
    => DrawLine(drawOrigin, end, Color.white, duration, depthTest);

    protected action DrawLine(Vector3 start, Vector3 end,
                              float duration = 0.0f,
                              bool depthTest = true)
    => DrawLine(start, end, Color.white, duration, depthTest);

    protected action DrawLine(Vector3 end, Color color,
                              float duration = 0.0f,
                              bool depthTest = true)
    => DrawLine(drawOrigin, end, color, duration, depthTest);

    protected action DrawLine(Vector3 start, Vector3 end,
                          Color color, float duration = 0.0f,
                          bool depthTest = true){
        Debug.DrawLine(start, end, color, duration, depthTest);
        return @void();
    }

    protected action DrawRay(Vector3 dir, float duration = 0.0f,
                             bool depthTest = true)
    => DrawRay(drawOrigin, dir, Color.white, duration, depthTest);

    protected action DrawRay(Vector3 start, Vector3 dir,
                             float duration = 0.0f,
                             bool depthTest = true)
        => DrawRay(start, dir, Color.white, duration, depthTest);

    protected action DrawRay(Vector3 dir, Color color,
                             float duration = 0.0f,
                             bool depthTest = true)
    => DrawRay(drawOrigin, dir, color, duration, depthTest);

    protected action DrawRay(Vector3 start, Vector3 dir,
                             Color color, float duration = 0.0f,
                             bool depthTest = true){
        Debug.DrawRay(start, dir, color, duration, depthTest);
        return @void();
    }

    protected action DrawPoint(Vector3 position,
                               float size = 0.05f,
                               float duration = 0.0f,
                               bool depthTest = true)
    => DrawPoint(position, Color.white, size, duration, depthTest);

    protected action DrawPoint(Vector3 position, Color color,
                               float size = 0.05f,
                               float duration = 0.0f,
                               bool depthTest = true){
        Vector3 X = Vector3.right * size,
                Y = Vector3.up * size,
                Z = Vector3.forward * size;
        DrawLine(position - X, position + X);
        DrawLine(position - Y, position + Y);
        DrawLine(position - Z, position + Z);
        return @void();
    }

    protected failure Err(object message, bool @break=false){
        Debug.LogWarning(message, gameObject);
        if(@break) Debug.Break();
        return failure.fail();
    }

    protected action Log(object message, bool @break=false){
        Debug.Log(message, gameObject);
        if(@break) Debug.Break();
        return @void();
    }

    protected action Warn(object message, bool @break=false){
        Debug.LogWarning(message, gameObject);
        if(@break) Debug.Break();
        return @void();
    }

    protected Vector3 drawOrigin => transform.position;

    // --------------------------------------------------------------

    Stepper stepper => GetComponentInParent<Stepper>();

}}
