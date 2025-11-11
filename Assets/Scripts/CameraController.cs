using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;
    [SerializeField] private float defaultAngle;

    public void Rotate(ObstacleObjects objects)
    {
        var targetY = objects switch
        {
            ObstacleObjects.LeftWall => leftAngle,
            ObstacleObjects.RightWall => rightAngle,
            ObstacleObjects.None => defaultAngle,
            _ => transform.localEulerAngles.y
        };
        var currentRotation = transform.eulerAngles;
        transform.DORotate(new Vector3(currentRotation.x, targetY, currentRotation.z), rotationSpeed).SetEase(Ease.Linear);
    }
}