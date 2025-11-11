using UnityEngine;

public class CameraObstacleMediator : BaseDetectionMediator
{
    [SerializeField] private CameraController cameraController;
    protected override void OnObstacleStateChanged(ObstacleObjects obstacle)
    {
        if (obstacle == ObstacleObjects.Light) return;
        cameraController.Rotate(obstacle);
        
    }
}