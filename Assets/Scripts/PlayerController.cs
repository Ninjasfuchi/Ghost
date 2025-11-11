using System;
using Sirenix.OdinInspector;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : SerializedMonoBehaviour
{
    [SerializeField] private SausagerStats stats;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private IDetector _detector;
    [SerializeField] private Transform startTransform;
    [SerializeField] private float respawnTime;
    private SausageInput _input;
    private SausageMovement _movement;
    private CharacterRespawner _respawn;
    
    private void Awake()
    {
        InitializeCore();
        _detector.OnObstacleStateChanged += HandleRotate;
        _detector.OnObstacleStateChanged += HandleRespawn;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize() =>  _movement.SetCanMove(true);
  
    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void InitializeCore()
    {
        _input = new SausageInput();
        _movement = new SausageMovement(rb,stats.movementSpeed,stats.rotationSpeed);
        _respawn = new CharacterRespawner(transform,startTransform,respawnTime);
       
    }

    //In Inspector
    public void OnMove(CallbackContext context) =>  _input.SetInput(context);
    
    private void HandleMovement()
    {
        _movement.Move(_input.MoveInput);
    }
    private void HandleRotate(ObstacleObjects obstacle)
    {
        if (obstacle == ObstacleObjects.Light) return;
        _movement.AutoRotate(obstacle);
    }
    
    private void HandleRespawn(ObstacleObjects obstacle)
    {
        if (obstacle != ObstacleObjects.Light) return;
        _movement.SetCanMove(false);
        StartCoroutine(_respawn.Respawn(() =>
        {
            _movement.SetCanMove(true);
        }));
    }


    private void OnDestroy()
    {
        _detector.OnObstacleStateChanged -= HandleRotate;
        _detector.OnObstacleStateChanged -= HandleRespawn;
    }
}