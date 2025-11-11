using System;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseDetectionMediator : SerializedMonoBehaviour
{
    [SerializeField] private IDetector _detector;

    private void OnEnable() => SubScribe();
    
    private void OnDisable() => UnsubScribe();
    
    private void SubScribe()
    {
        _detector.OnObstacleStateChanged += OnObstacleStateChanged;
    }

    private void UnsubScribe()
    {
        _detector.OnObstacleStateChanged -= OnObstacleStateChanged;
    }
    
    protected abstract void OnObstacleStateChanged(ObstacleObjects obstacle);
}