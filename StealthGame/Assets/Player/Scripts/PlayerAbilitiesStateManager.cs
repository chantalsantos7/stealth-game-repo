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
    public PlayerDistractingState distractingState = new PlayerDistractingState();

    [Header("Player Script References")]
    public PlayerLocomotion playerLocomotion;
    public PlayerManager player;
    public InputManager inputManager;
    
    [Header("Scene References")]
    public CameraManager cameraManager;
    public GameObject teleportView;
    public Rigidbody teleportRigidbody;
    public ParticleSystem teleportParticles;
    public GameObject distractorPreview;

    [Header("Prefabs")]
    public GameObject distractorObj;
    //public Rigidbody distractRigidbody;

    /*[Header("Player Stats")]
    public int maxStamina = 75;
    public float maxHealth = 100f;*/

    [Header("Ability Cooldowns")]
    [Tooltip("How many seconds before the teleport ability is available to the player again")]
    public float teleportCooldown = 10;
    [Tooltip("How many seconds before the distract ability is available to the player again")]
    public float distractionCooldown = 15;
    public bool teleportAllowed;
    public bool distractionAllowed;
    [HideInInspector] public float teleportTimeElapsed; //set to 11 at initialisation so player can teleport immediately upon entering game
    [HideInInspector] public float distractTimeElapsed;


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

        distractionAllowed = true;
        distractTimeElapsed = 11;
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
    }

    public void SwitchState(PlayerAbilitiesState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
}
