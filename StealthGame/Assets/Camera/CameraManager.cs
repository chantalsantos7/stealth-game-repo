using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public bool inCombat;
    public CameraMode cameraMode;
    public Transform combatLookAt;
    /*public CinemachineFreeLook combatCam;*/
    public CinemachineFreeLook basicCam;
    public GameObject aimCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //cameraMode = inCombat ? CameraMode.Combat : CameraMode.Basic;
        switch (cameraMode)
        {
            case CameraMode.AimTeleport:
                basicCam.gameObject.SetActive(false);
                aimCam.SetActive(true);
                //turn off player movement while in this mode, to prevent weird movement
                break;
            case CameraMode.Basic:
                basicCam.gameObject.SetActive(true);
                aimCam.SetActive(false);
                break;
        }
    }
}
