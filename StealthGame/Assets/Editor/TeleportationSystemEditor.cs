using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerAbilitiesStateManager))]
public class TeleportationSystemEditor : Editor
{
    private void OnSceneGUI()
    {
        PlayerAbilitiesStateManager player = (PlayerAbilitiesStateManager)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(player.transform.position, Vector3.up, Vector3.forward, 360, player.teleportRadiusLimit);
    }
}
