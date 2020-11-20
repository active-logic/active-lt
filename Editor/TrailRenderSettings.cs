using UnityEngine;
using static UnityEditor.EditorPrefs;
using Key = Active.Editors.ALEditorPrefs;

namespace Active.Editors{
public class TrailRenderSettings{

    public float offset{
        set => SetFloat(Key.Offset, value);
        get => GetFloat(Key.Offset, 0.01f);
    }

    public float size{
        set => SetFloat(Key.Size, value);
        get => GetFloat(Key.Size, 0.1f);
    }

    public float pickSize => size * 2;

    public Color color{
        set{
            SetFloat(Key.Color+'R', value.r); SetFloat(Key.Color+'G', value.g);
            SetFloat(Key.Color+'B', value.g);
        }
        get => new Color(
            GetFloat(Key.Color + 'R'), GetFloat(Key.Color + 'G'),
            GetFloat(Key.Color + 'B')
        );

    }

}}
