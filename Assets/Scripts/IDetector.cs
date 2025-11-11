using System;

public interface IDetector
{ 
    event Action<ObstacleObjects> OnObstacleStateChanged;
}