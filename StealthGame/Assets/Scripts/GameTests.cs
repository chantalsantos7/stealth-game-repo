using Assets.Scripts.Enumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTests : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public CameraManager cameraManager;

    [Header("Tests")]
    public bool resetPosition;
    public bool testCombatCam;
    public bool enemyCamView;

    Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = player.position;
    }

    void Update()
    {
        if (resetPosition)
        {
            ResetPosition();
            resetPosition = false;
        }

        if (testCombatCam)
        {
            TestCombatCam();
        }
    }

    void ResetPosition()
    {
        player.position = originalPosition;
    }

    void TestCombatCam()
    {
        if (cameraManager.cameraMode == CameraMode.Combat)
        {
            cameraManager.cameraMode = CameraMode.Basic;
            testCombatCam = false;
        } else
        {
            cameraManager.cameraMode = CameraMode.Combat;
        }
    }

    /*void EnemyCamView()
    {
        if (cameraManager.cameraMode == CameraMode.EnemyView)
        {
            cameraManager.cameraMode = CameraMode.Basic;
            enemyCamView = false;
        }
        else
        {
            cameraManager.cameraMode = CameraMode.EnemyView;
        }
    }*/
}
