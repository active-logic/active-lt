using Ex = System.Exception;
using UnityEngine;
using Active.Core; using static Active.Status;

namespace Active.Util{
public class MecanimDriver : AnimationDriver{

    Animator α;
    string current = null;

    public MecanimDriver(Animator x){
        α = x;
        α.applyRootMotion = false;
    }

    override public pending DoPlay(string anim){
        if(current != anim){
            current = anim;
            PlayOrCrossFade(anim, fadeLength);
            return pending.cont();
        }
        return IsPlaying(anim) ? pending.cont() : pending.done();
    }

    override public void DoLoop(string anim){
        if(current != anim){
            current = anim;
            PlayOrCrossFade(anim, fadeLength);
        }else{
            var s = α.GetCurrentAnimatorStateInfo(0);
            // May drop 's.IsName(anim)'; anim current is implied.
            // When forcing a loop, do not cross-fade; need more
            // info (parameter?) to know whether the animation can
            // loop seamlessly.
            if(!s.loop && s.normalizedTime >= 1 && s.IsName(anim))
                α.Play(anim, layer: -1, normalizedTime: 0f);
        }
    }

    override public bool Exists(string anim)
        => throw new Ex("Unimplemented");

    // τ: normalizedTimeOffset; -1: default value for layer
    void PlayOrCrossFade(string anim, float μ, float τ){
        if(μ <= 0) α.Play(anim, layer: -1, normalizedTime: τ);
        else
            α.CrossFadeInFixedTime(anim,
                                   fixedTransitionDuration: μ,
                                   layer: -1,
                                   fixedTimeOffset: 0f,
                                   normalizedTransitionTime: τ);
    }

    void PlayOrCrossFade(string anim, float μ){
        if(μ <= 0){
            α.Play(anim);
        }else{
            α.CrossFadeInFixedTime(anim,
                                   fixedTransitionDuration: μ,
                                   layer: -1);
        }
    }

    bool IsPlaying(string anim){
        var s = α.GetCurrentAnimatorStateInfo(0);
        if(!s.IsName(anim)){
            // perhaps counter-intuitive, when the animation state is
            // not current, it normally indicates it is not current
            // *yet*. NOTE: for safety (and likely slower) check
            // GetNextAnimatorStateInfo(0)
            return true;
        }
        return s.loop || (s.normalizedTime < 1 - fadeLength/s.length);
    }

}}
