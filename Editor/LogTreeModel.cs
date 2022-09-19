#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

#if !AL_OPTIMIZE

using UnityEngine; using UnityEditor;
using Active.Core; using Active.Core.Details;
using static UnityEditor.EditorPrefs;
using Ed = UnityEditor.EditorApplication;
using Key = Active.Editors.ALEditorPrefs;

namespace Active.Editors{
public class LogTreeModel{

    int? _frame;
    public bool  step        = false;
    public readonly TrailRenderSettings renderSettings
        = new TrailRenderSettings();

    public string output => playing || (frame == Time.frameCount)
                            ? currentOutput : historyOutput;

    public bool enable{
        set => SetBool(Key.EnableLogging, value);
        get => GetBool(Key.EnableLogging, true);
    }

    public bool trails{
        set => SetBool(Key.Trails, value);
        get => GetBool(Key.Trails, true);
    }

    public int? frame{
        set => _frame = value;
        get => !Application.isPlaying ? (int?)null
               : Ed.isPaused          ? (_frame ?? Time.frameCount)
               : Time.frameCount;
    }

    public GameObject active => Selection.activeGameObject;

    public LogSource[] roots => active.GetComponents<LogSource>();

    public void SelectPreviousFrame(){
        if(playing || !active || frame == null) return;
        int? sel = null;
        foreach(var s in roots){
            var n = s.GetHistory()?.Previous(frame.Value)?.frame;
            if(n != null && (!sel.HasValue || sel.Value < n)) sel = n;
        } frame = sel ?? frame;
    }

    public void SelectNextFrame(){
        if(playing || !active || frame == null) return;
        int? sel = null;
        foreach(var s in roots){
            int? n = s.GetHistory()?.Next(frame.Value)?.frame;
            if(n != null && (!sel.HasValue || sel.Value > n)) sel = n;
        } frame = sel ?? frame;
    }

    // ------------------------------------------------------------------------

    string historyOutput{ get{
        if(active == null) return "Nothing selected";
        var @out = "";
        foreach(var s in roots) if(s.GetHistory() != null) @out += HistoryOutput(s);
        return @out;
    }}

    string currentOutput{ get{
        int count = ActiveStepperCount(active);
        if(count == 0){
            return "Enable logging in Stepper to start";
        }else{
            var @out = "";
            foreach(var k in roots){
                if(k.IsLogging() && !string.IsNullOrEmpty(k.GetLog())){
                    if(count > 1) @out += k.GetType().Name + '\n';
                    @out += k.GetLog() + '\n';
                }
            } return @out;
        }
    }}

    bool playing => Application.isPlaying && !Ed.isPaused;

    string HistoryOutput(LogSource s) => frame.HasValue
        ? s.GetHistory()[frame.Value].log
        : null;

    int ActiveStepperCount(GameObject go){
        var steppers = go?.GetComponents<LogSource>();
        if(steppers == null || steppers.Length == 0) return 0;
        int c = 0;
        foreach(var k in steppers) if(k.IsLogging()) c++;
        return c;
    }

}}

#endif  // !AL_OPTIMIZE
