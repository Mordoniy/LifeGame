using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SimpleEditor : Editor
{

    [MenuItem("Edit/CustomPlay _F5")]
    public static void Play()
    {
        EditorApplication.isPlaying = !EditorApplication.isPlaying;
    }

    [MenuItem("Edit/CustomPause _F6")]
    public static void Pause()
    {
        EditorApplication.isPaused = !EditorApplication.isPaused;
    }
}