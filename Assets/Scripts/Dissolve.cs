using DG.Tweening;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private Material material;
    private static readonly int Diss = Shader.PropertyToID("_Float");

    private Tween _currentTween;

    public void SetValue(float value, float duration)
    {
        _currentTween?.Kill();
        
        _currentTween = material.DOFloat(value, Diss, duration)
            .SetEase(Ease.Linear);
    }
}