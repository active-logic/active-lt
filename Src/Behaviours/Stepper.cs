// Doc/Reference/Steppers.md
#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

using System;
using System.Linq;
using UnityEngine;
using Active.Core.Details;
using static Active.Status;
using U = UnityEngine.SerializeField;

namespace Active.Core{
[UnityEngine.AddComponentMenu("Active Logic/Stepper")]
public class Stepper : MonoBehaviour, LogSource{

    public UGig root;
    public bool loop = true;
    [HideInInspector]
    public int leniency = 1;
    [HideInInspector] public string log;
    //
    #if(!AL_OPTIMIZE)
    [Header("Log")]
    public bool enableLogging = false;
    public bool useHistory = false;
    [Header("Breakpoints")]
    [U] bool  useBreakpoints = true;
    [U] bool  step           = false;
    [U] string[] breaks      = new string[0];
    //
    History _history;
    #endif
    float pause;
    Action command;
    int frame;

    // -------------------------------------------------------------

    #if !AL_OPTIMIZE

    public bool isLogging => enableLogging && Application.isPlaying;

    public History history => (isLogging && useHistory)
        ? _history ?? (_history = new History())
        : _history = null;

    bool breaksFound
    => breaks.FirstOrDefault(x => log.Contains(x)) != null;

    #endif

    bool shouldReenable => loop && command==null;

    // --------------------------------------------------------------

    void Start(){
        if(root != null) return;
        string candidates = null;
        bool unique = true;
        foreach(var e in GetComponents<UGig>()){
            if(e.rootable){
                if(root == null) root = e;
                if(candidates != null){
                    candidates += ", ";
                    unique = false;
                }
                candidates += e.ToString();
            }
        }
        if(!unique){
            throw new System.Exception($"Multiple candidates for root: {candidates}");
        }
    }

    public loop Pause(float duration){
        if(duration <= 0) throw
            new Exception("Pause duration must be > 0");
        pause   = duration;
        command = DoPause;
        return Active.Core.loop.cont();
    }

    public action Suspend(){ command = DoSuspend; return @void(); }

    public action Resume(){ enabled = true; return @void(); }

    public status Step(){
        SimTime.time = UnityEngine.Time.time;
        status s = fail();
        #if !AL_OPTIMIZE
        status.log = enableLogging;
        #endif
        if(command != DoSuspend && command != DoPause){
            if(root){
                RoR.Enter(this, frame, leniency);
                try{
                    s = root;
                }catch(Exception){
                    RoR.Exit(this, ref frame);
                    throw;
                }
                RoR.Exit(this, ref frame);
            }else{
                throw new Exception($"Rootless: {gameObject.name}");
            }
            if(!s.running) enabled = shouldReenable;
            #if !AL_OPTIMIZE
            if(enableLogging) Log(s);
            #endif
        }
        if(command != null){ command(); command = null; }
        return s;
    }

    // NOTE: 'inferNext' is needed because `Push` may be
    // called before `Start` which may auto-assign a root task
    public loop Push(Func<status> φ, bool inferNext=true){
        var next = root;
        if(inferNext && root == null) next = GetComponent<UGig>();
        var gig = gameObject.AddComponent<UGigAdapter>();
        gig.call = φ; gig.next = next; root = gig;
        return Active.Core.loop.cont();
    }

    public virtual void Run(Func<status> φ){
        var adapter = GetComponent<UGigAdapter>();
        if(!adapter)
            adapter = gameObject.AddComponent<UGigAdapter>();
        adapter.call = φ;
        root = adapter;
    }

    void Log(status s){
        #if !AL_OPTIMIZE
        var previousLog = log;
        log = StatusFormat.CallTree(s);
        Logger.RequestPaint();
        if(step && History.TrimLog(log) != History.TrimLog(previousLog))
            Debug.LogError($"Step {gameObject.name}");
        if(useHistory) history.Log(log, transform);
        if(!step && useBreakpoints && breaksFound)
            Debug.LogError($"Break in {gameObject.name}");
        #endif
    }

    void DoPause()
    { CancelInvoke(); Invoke("Resume", pause); enabled = false; pause = 0; }

    void DoSuspend(){ CancelInvoke(); enabled = false; }

    // <LogSource> ==================================================

    History LogSource.GetHistory() => history;
    bool    LogSource.IsLogging()  => isLogging;
    string  LogSource.GetLog()     => log;

}}
