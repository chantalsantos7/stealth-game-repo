using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DistractionSystem))]
public class DistractionSystemEditor : Editor
{
    private void OnSceneGUI()
    {
        DistractionSystem distractor = (DistractionSystem)target;
        Handles.color = Color.cyan;
        Handles.DrawWireArc(distractor.transform.position, Vector3.up, Vector3.forward, 360, distractor.enemyRange);
    }
}
