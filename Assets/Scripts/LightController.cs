using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LightController : SerializedMonoBehaviour
{
    [SerializeField] private List<LightDetection> detections;
    [SerializeField] private List<LightMovement> movements;
    [SerializeField] private List<IDetector> _detectors;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Subscribe()
    {
        foreach (var detector in _detectors)
            detector.OnObstacleStateChanged += SetCanMove;
    }

    private void Unsubscribe()
    {
        foreach (var detector in _detectors)
            detector.OnObstacleStateChanged -= SetCanMove;
    }

    private void SetCanMove(ObstacleObjects obstacle)
    {
        foreach (var move in movements)
        {
            var det = detections.Find(d => d.Index == move.Index);
            move.SetCanMove(det && det.IsDetected);
        }
    }
    

}