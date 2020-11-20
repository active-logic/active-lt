using UnityEngine;
using Active.Core; using static Active.Status;

namespace Active.Util{
public class LegacyAnimationDriver : AnimationDriver{

    Animation α;
    string current = null;

    public LegacyAnimationDriver(Animation x) => α = x;

    override public pending DoPlay(string anim){
        if(current != anim){
            current = anim;
            PlayOrCrossFade(anim);
        }
        return IsPlaying(anim, fadeLength)
               ? pending.cont() : pending.done();
    }

    override public void DoLoop(string anim){
        current = anim;
        PlayOrCrossFade(anim);
    }

    override public bool Exists(string anim){
        return α[anim] != null;
    }

    void PlayOrCrossFade(string anim){
        if(fadeLength > 0f) α.CrossFade(anim); else α.Play(anim);
    }

    bool IsPlaying(string anim, float fadeLength){
        if(!α.IsPlaying(anim)) return false;
        var state = α[anim];
        return ((fadeLength > 0) && Finite(state))
            ? (state.time < state.length - fadeLength) : true;
    }

    bool Finite(AnimationState state){
        var w = state.wrapMode;
        if(w == WrapMode.Default) w = α.wrapMode;
        return w == WrapMode.Once;
    }

}}
