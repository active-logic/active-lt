using ArgEx = System.ArgumentException;
using UnityEngine;
using Active.Core; using static Active.Status;

namespace Active.Util{
public abstract class AnimationDriver{

    public float fadeLength = 0.3f;

    public abstract bool Exists(string anim);

    public pending Play(string anim){
        if(string.IsNullOrEmpty(anim))
            throw new ArgEx("'anim' cannot be null");
        return DoPlay(anim);
    }

    public loop Loop(string anim){
        if(string.IsNullOrEmpty(anim))
            throw new ArgEx("'anim' cannot be null");
        DoLoop(anim);
        return loop.cont();
    }

    public abstract pending DoPlay(string anim);

    public abstract void DoLoop(string anim);

}}
