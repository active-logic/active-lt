// Doc/Reference/Steppers.md
#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

namespace Active.Core{
[UnityEngine.AddComponentMenu("Active Logic/Physics Agent")]
public class PhysicsAgent : Stepper{

    void FixedUpdate() => Step();

}}
