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
    public GameObject basicCam;
    public GameObject crouchCam;
    public GameObject aimTeleportCam;
    public GameObject aimDistractorCam;

    // Update is called once per frame
    void Update()
    {
        switch (cameraMode)
        {
            case CameraMode.AimTeleport:
                basicCam.SetActive(false);
                aimTeleportCam.SetActive(true);
                //turn off player movement while in this mode, to prevent weird movement
                break;
            case CameraMode.Basic:
                basicCam.SetActive(true);
                aimTeleportCam.SetActive(false);
                aimDistractorCam.SetActive(false);
                break;
            case CameraMode.AimDistractor:
                basicCam.SetActive(false);
                aimDistractorCam.SetActive(true);
                break;
            case CameraMode.Crouch:
                basicCam.SetActive(true);
                crouchCam.SetActive(true);
                break;
        }
    }
}
