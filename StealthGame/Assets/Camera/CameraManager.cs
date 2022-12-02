using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    //public bool inCombat;
    public CameraMode cameraMode;
    public Transform combatLookAt;
    public CinemachineFreeLook basicCam;
    public GameObject enemyCam;
    public GameObject aimCam;

    // Update is called once per frame
    void Update()
    {
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
            case CameraMode.EnemyView:
                basicCam.gameObject.SetActive(false);
                enemyCam.SetActive(true);
                break;
        }
    }
}
