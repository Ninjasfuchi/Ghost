using System;
using DG.Tweening;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private int index;
    private Tween _tween;
    private bool _isStoppedByDetection = false;

    public int Index => index;

    private void Start() => StartMove();


    private void StartMove()
    {
        if (_tween != null) return;

        var targetPos = transform.position + direction;
        _tween = transform.DOMove(targetPos, 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void SetCanMove(bool detected)
    {
        if (detected)
        {
            if (_isStoppedByDetection || _tween == null) return;
            _tween.Pause();
            _isStoppedByDetection = true;
        }
        else
        {
            if (!_isStoppedByDetection || _tween == null) return;
            _tween.Play();
            _isStoppedByDetection = false;
        }
    }
}