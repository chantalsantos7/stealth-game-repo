using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public bool inCombat;
    public CameraMode cameraMode;
    public Transform combatLookAt;
    public CinemachineFreeLook combatCam;
    public CinemachineFreeLook basicCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraMode = inCombat ? CameraMode.Combat : CameraMode.Basic;
        if (cameraMode == CameraMode.Combat)
        {
            /*Vector3 viewDirection = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);*/
            basicCam.gameObject.SetActive(false);
            combatCam.gameObject.SetActive(true);
        } 
        else
        {
            basicCam.gameObject.SetActive(true);
            combatCam.gameObject.SetActive(false);
        }
       /* if (inCombat)
        {
            cameraMode = CameraMode.Combat;
        } else
        {
            cameraMode = CameraMode.Basic;
        }*/
    }
}
