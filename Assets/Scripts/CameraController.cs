using System;
using Cinemachine;
using DG.Tweening;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;
    [SerializeField] private float defaultAngle;
    [SerializeField] private Camera cam;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private CinemachineFramingTransposer transposer;
   

    private void Awake()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void Rotate(ObstacleObjects objects)
    {
        float targetY;
        switch (objects)
        {
            case ObstacleObjects.LeftObstacle:
                UpdateCamSettings(ObstacleObjects.LeftObstacle);
                targetY = leftAngle;
                break;
            case ObstacleObjects.RightObstacle:
                UpdateCamSettings(ObstacleObjects.RightObstacle);
                targetY = rightAngle;
                break;
            case ObstacleObjects.None:
                UpdateCamSettings(ObstacleObjects.None);
                targetY = defaultAngle;
                break;
            default:
                targetY = transform.localEulerAngles.y;
                break;
        }

        var currentRotation = transform.eulerAngles;
        transform.DORotate(new Vector3(currentRotation.x, targetY, currentRotation.z), rotationSpeed).SetEase(Ease.Linear);
    }

    
    private void UpdateCamSettings(ObstacleObjects objects)
    {
        switch (objects)
        {
            case ObstacleObjects.LeftObstacle:
                cam.cullingMask &= ~LayerMask.GetMask($"RightWall");
                transposer.m_CameraDistance = maxDistance;
                break;

            case ObstacleObjects.RightObstacle:
                cam.cullingMask &= ~LayerMask.GetMask($"LeftWall");
                transposer.m_CameraDistance = maxDistance;
                break;

            case ObstacleObjects.None:
                cam.cullingMask |= LayerMask.GetMask($"RightWall",$"LeftWall");
                transposer.m_CameraDistance = minDistance;
                break;
        }
    }

}