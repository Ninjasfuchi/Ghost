using UnityEngine;

public class LightObstacleMediator : BaseDetectionMediator
{
    [SerializeField] private Dissolve dissolve;
    [SerializeField] private float duration;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    private float _currentValue;
    protected override void OnObstacleStateChanged(ObstacleObjects obstacle)
    {
        switch (obstacle)
        {
            case ObstacleObjects.Light:
                _currentValue = maxValue;
                dissolve.SetValue(_currentValue, duration);
                break;
            case ObstacleObjects.None when !Mathf.Approximately(_currentValue, minValue):
                _currentValue = minValue;
                dissolve.SetValue(_currentValue, duration);
                break;
        }
    }

}