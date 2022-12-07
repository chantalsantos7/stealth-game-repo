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
    
    [Header("Player Stats")]
    public int maxStamina = 75;
    public float maxHealth = 100f;

    public float health;
    public int currentStamina;

    [Header("Ability Cooldowns")]
    public float teleportCooldown = 10;
    public float staminaCooldown;
    public bool teleportAllowed;
    [HideInInspector] public float teleportTimeElapsed; //set to 11 at initialisation so player can teleport immediately upon entering game


    bool sprinting;
    public WaitForSeconds regenTick = new WaitForSeconds(0.5f);

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        teleportAllowed = true;
        teleportTimeElapsed = 11; //set to 11 at initialisation so player can teleport immediately upon entering game
        teleportRigidbody = teleportView.GetComponent<Rigidbody>();
    }

    void Start()
    {
        currentState = baseState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        //if current state was 
        
        
    }

    public void SwitchState(PlayerAbilitiesState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
