using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetTransform; //the trasnform of object camera will follow
    public Transform cameraTransform; //the transform of the camera
    public Transform cameraPivot; //the object the transform uses to pivot
    public LayerMask collisionLayers;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private float defaultPosition;
    private Vector3 cameraVectorPosition;

    [SerializeField] private float cameraFollowSpeed = 0.2f;
    [SerializeField] private float cameraLookSpeed = 2;
    [SerializeField] private float cameraPivotSpeed = 2;
    [SerializeField] private float minimumPivotAngle = -35;
    [SerializeField] private float maximumPivotAngle = 35;

    public float cameraCollisionRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f; //distance camera will move after colliding w/ object
    public float minimumCollisionOffset = 0.2f;
    public float lookAngle;
    public float pivotAngle;

    InputManager inputManager;

    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation = Vector3.zero;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraHorizontalInput * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraVerticalInput * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);
        
       
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
        

    }

    //Will bring camera closer to player if it collides with something (i.e a wall)
    private void HandleCameraCollisions() 
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();
        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
