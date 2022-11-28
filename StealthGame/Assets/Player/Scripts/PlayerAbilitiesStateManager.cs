using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerAbilitiesStateManager : MonoBehaviour
{
    public PlayerAbilitiesState currentState;
    public PlayerAbilitiesBaseState baseState = new PlayerAbilitiesBaseState();
    public PlayerTeleportingState teleportingState = new PlayerTeleportingState();
    public PlayerSprintingState sprintingState = new PlayerSprintingState();

    [Header("Object References")]
    public PlayerLocomotion playerLocomotion;
    public PlayerManager player;
    public InputManager inputManager;
    public GameObject teleportView;
    public CameraManager cameraManager;
    public Rigidbody teleportRigidbody;
    public ParticleSystem teleportParticles;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        
        teleportRigidbody = teleportView.GetComponent<Rigidbody>();
    }

    //recharge sprinting in this state?

    void Start()
    {
        currentState = baseState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerAbilitiesState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
