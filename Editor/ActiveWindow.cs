using UnityEditor;
using UnityEngine;

namespace Active.Editors{
public class ActiveWindow : EditorWindow{

    protected Vector2 scroll;
    int lastRepaint;

    public void RequestPaint(){
        if(Time.frameCount == lastRepaint) return;
        Repaint();
        lastRepaint = Time.frameCount;
    }

    protected void Title(string str)
    => GUILayout.Label(str, EditorStyles.boldLabel);

    protected void Label(string str) => GUILayout.Label(str);

    protected void print(string str) => Debug.Log(str);

}}
