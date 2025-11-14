using System;
using UnityEngine;
using System.Collections;

public class CharacterDetection : MonoBehaviour, IDetector
{
    [SerializeField] private LayerMask leftObstacle;
    [SerializeField] private LayerMask rightObstacle;
    [SerializeField] private LayerMask lightObstacle;
    [SerializeField] private float exitDelay;
    private Coroutine _exitCoroutine;
    
    public event Action<ObstacleObjects> OnObstacleStateChanged;

    

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & leftObstacle) != 0)
        {
            StopExitCoroutine();
            OnObstacleStateChanged?.Invoke(ObstacleObjects.LeftObstacle);
        }
        else if (((1 << other.gameObject.layer) & rightObstacle) != 0)
        {
            StopExitCoroutine();
            OnObstacleStateChanged?.Invoke(ObstacleObjects.RightObstacle);
        }
        
        else if (((1 << other.gameObject.layer) & lightObstacle) != 0)
        {
            StopExitCoroutine();
            OnObstacleStateChanged?.Invoke(ObstacleObjects.Light);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & leftObstacle) == 0 &&
            ((1 << other.gameObject.layer) & rightObstacle) == 0 &&
            ((1 << other.gameObject.layer) & lightObstacle) == 0) return;
        StopExitCoroutine();
        _exitCoroutine = StartCoroutine(DelayedExit());
    }


    private IEnumerator DelayedExit()
    {
        yield return new WaitForSeconds(exitDelay);
        OnObstacleStateChanged?.Invoke(ObstacleObjects.None);
    }

    private void StopExitCoroutine()
    {
        if (_exitCoroutine == null) return;
        StopCoroutine(_exitCoroutine);
        _exitCoroutine = null;
    }
}