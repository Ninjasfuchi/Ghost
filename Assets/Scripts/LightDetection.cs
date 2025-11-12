
using System;
using UnityEngine;

public class LightDetection : MonoBehaviour,IDetector
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private bool isDetected;
    [SerializeField] private int index;
    
    public event Action<ObstacleObjects> OnObstacleStateChanged;
    
    public bool IsDetected => isDetected;
    public int Index => index;
    

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) == 0) return;
        isDetected = true;
        OnObstacleStateChanged?.Invoke(ObstacleObjects.Character);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) == 0) return;
        isDetected = false;
        OnObstacleStateChanged?.Invoke(ObstacleObjects.Character);
    }
}