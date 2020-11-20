#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

#if !AL_OPTIMIZE

using UnityEngine; using UnityEditor;
using Active.Core; using Active.Core.Details;
using static UnityEngine.Vector3;
using static UnityEditor.Handles;
using Settings = Active.Editors.TrailRenderSettings;

namespace Active.Editors{
public class ALHistoryGUI{

    public static int? Draw(LogTreeModel target){
        var rs = target.renderSettings; color = rs.color; int? sel = null;
        foreach(var s in target.roots) if(s.history != null)
            Draw(s, rs, ref sel);
        return sel;
    }

    public static void DrawSelected(LogTreeModel target){
        if(target.frame == null) return; color = Color.red;
        foreach(var s in target.roots) if(s.history!=null){
            var f = target.frame.Value;
            Emphasis(s.history[target.frame.Value], target.renderSettings);
        }
    }

    public static int? Draw(Stepper s, Settings rs, ref int? sel){
        VTrace prev = null; foreach(VTrace x in s.history){
            if(prev != null) DrawLine(Pos(prev, rs), Pos(x, rs));
            if(Button(Pos(x, rs), Rot(x), rs.size, rs.pickSize,
                                          RectangleHandleCap)) sel = x.frame;
            prev = x;
        } return sel;
    }

    static void Emphasis(VTrace x, Settings s){
        if(x == null) return;
        Button(Pos(x, s), Rot(x), s.size, s.pickSize, CubeHandleCap);
    }

    static Vector3    Pos(VTrace x, Settings s) => x.position + up * s.offset;
    static Quaternion Rot(VTrace x) => x.rotation;

}}

#endif  // !AL_OPTIMIZE
