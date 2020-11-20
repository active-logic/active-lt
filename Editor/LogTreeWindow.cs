// Doc/Reference/Steppers.md
#if !(UNITY_EDITOR || DEBUG)
#define AL_OPTIMIZE
#endif

using UnityEngine;
using UnityEditor;
using static UnityEditor.EditorGUILayout;
using Ed = UnityEditor.EditorApplication;
using GL = UnityEngine.GUILayout;
using System.Linq; // for string[].Intersect

namespace Active.Editors{
public class LogTreeWindow : EditorWindow{

    public static LogTreeWindow instance;
    const int FontSize = 11;

    #if !AL_OPTIMIZE
    static Font     _font;
    Vector2         scroll;
    LogTreeModel    model = new LogTreeModel();

    LogTreeWindow(){
        Ed.pauseStateChanged +=
         (PauseState s) => { if(s == PauseState.Paused) Repaint(); };
    }

    string frameNo => model.frame.HasValue ? $"#{model.frame}" : "...";

    void OnFocus(){
        #if UNITY_2019
        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;
        #else
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        #endif
    }

    void OnGUI(){
        if(!model.enable)
        { model.enable = ToggleLeft("Log", model.enable); return; }
        instance = this;  // TODO: why do this here?
        BeginHorizontal();
        if(GL.Button("˂", GL.ExpandWidth(false))){
            model.SelectPreviousFrame();
            SceneView.RepaintAll();
        }
        GL.Button(frameNo, GL.MaxWidth(48f));
        if(GL.Button("˃", GL.ExpandWidth(false))){
            model.SelectNextFrame();
            SceneView.RepaintAll();
        }
        GL.FlexibleSpace();
        EndHorizontal();
        //
        scroll = BeginScrollView(scroll);
        GUI.backgroundColor = Color.black;
        var style = GUI.skin.textArea;
        var f = font;
        if(f == null) Debug.LogError("font not available");
        style.font = f;
        style.fontSize = FontSize;
        style.normal.textColor  = Color.white * 0.9f;
        style.focused.textColor = Color.white;
        style.focused.textColor = Color.white;
        GL.TextArea(model.output, GL.ExpandHeight(true));
        EndScrollView();
        //
        GUI.backgroundColor = Color.white;
        //
        var w30 = GL.MaxWidth(30f);
        BeginHorizontal();
        model.enable = ToggleLeft($"Log", model.enable, GL.MaxWidth(60));
        model.trails = ToggleLeft("Trails", model.trails, GL.MaxWidth(50));
        GL.Label("Offset: ", GL.ExpandWidth(false));
        var rs = model.renderSettings;
        rs.offset = FloatField(model.renderSettings.offset, w30);
        GL.Label("Size: ", w30);
        rs.size = FloatField(rs.size, w30);
        GL.Label("Col: ", w30);
        rs.color = EditorGUILayout.ColorField(rs.color);
        EndHorizontal();
    }

    void OnInspectorUpdate() => Repaint();

    void OnSceneGUI(SceneView sceneView){
        if(!model.trails || !model.active) return;
        var sel = ALHistoryGUI.Draw(model);
        if(Ed.isPaused || !Application.isPlaying){
            model.frame = sel ?? model.frame;
            Repaint();
        }else model.frame = null;
        ALHistoryGUI.DrawSelected(model);
    }

    void OnSelectionChange()
    { if(Ed.isPaused || !Application.isPlaying) Repaint(); }

    static Font font{ get{
        if(_font) return _font;
        var avail = new []{
            "Menlo", "Consolas", "Inconsolata", "Bitstream Vera Sans Mono",
            "Oxygen Mono", "Ubuntu Mono", "Cousine", "Courier", "Courier New",
            "Lucida Console", "Monaco"
        }.Intersect(Font.GetOSInstalledFontNames()).First();
        return _font = Font.CreateDynamicFontFromOSFont(avail, FontSize);
    }}

    bool browsing
    => (Ed.isPaused || !Application.isPlaying) && model.frame > 0;

    #else  // AL_OPTIMIZE

    void OnGUI(){
        GL.Label("Unavailable in optimized mode");
    }

    #endif

    [MenuItem("Window/Activ/Log-Tree")]
    static void Init(){
        instance = (LogTreeWindow)EditorWindow
                   .GetWindow<LogTreeWindow>(title: "Log-Tree");
        instance.Show();
    }

}}
