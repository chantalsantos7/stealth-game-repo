using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DetectionSystem))]
public class DetectionSystemEditor : Editor
{
    private void OnSceneGUI()
    {
        DetectionSystem detector = (DetectionSystem)target;
        
        //sight detection radius
        Handles.color = Color.red;
        Handles.DrawWireArc(detector.transform.position, Vector3.up, Vector3.forward, 360, detector.sightDetectionRadius);
        
        //hearing detection radius
        Handles.color = Color.green;
        Handles.DrawWireArc(detector.transform.position, Vector3.up, Vector3.forward, 360, detector.hearingDetectionRadius);

        //attack radius
        Handles.DrawWireArc(detector.transform.position, Vector3.up, Vector3.forward, 360, detector.attackRadius);

        Vector3 viewAngle01 = DirectionFromAngle(detector.transform.eulerAngles.y, -detector.detectionAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(detector.transform.eulerAngles.y, detector.detectionAngle / 2);

        Handles.color = Color.blue;
        Handles.DrawLine(detector.transform.position, detector.transform.position + viewAngle01 * detector.sightDetectionRadius);
        Handles.DrawLine(detector.transform.position, detector.transform.position + viewAngle02 * detector.sightDetectionRadius);


        if (detector.canSeePlayer)
        {
            Handles.color = Color.magenta;
            Handles.DrawLine(detector.transform.position, detector.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    
}
