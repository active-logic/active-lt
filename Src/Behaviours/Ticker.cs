// Doc/Reference/Steppers.md
#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

namespace Active.Core{
[UnityEngine.AddComponentMenu("Active Logic/Ticker")]
public class Ticker : Stepper{

    public float firstTime  = 0f;
    public float repeatRate = 1f;

    void OnEnable() => InvokeRepeating("Step", firstTime, repeatRate);

    void OnDisable() => CancelInvoke();

}}
