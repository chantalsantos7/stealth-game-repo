using Assets.Scripts.Enumerators;
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
    public GameObject aimTeleportCam;
    public GameObject aimDistractorCam;

    // Update is called once per frame
    void Update()
    {
        switch (cameraMode)
        {
            case CameraMode.AimTeleport:
                basicCam.gameObject.SetActive(false);
                aimTeleportCam.SetActive(true);
                //turn off player movement while in this mode, to prevent weird movement
                break;
            case CameraMode.Basic:
                basicCam.gameObject.SetActive(true);
                aimTeleportCam.SetActive(false);
                aimDistractorCam.SetActive(false);
                break;
            case CameraMode.AimDistractor:
                basicCam.gameObject.SetActive(false);
                aimDistractorCam.SetActive(true);
                break;
        }
    }
}
