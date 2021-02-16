using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenController : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 0.1f;
    [SerializeField] private float _zMovement = 0.003f;
    [SerializeField] private Ease _moveEase = Ease.Linear;
    private Vector3 _targetLocation;
    private Vector3 _originalLocation;

    public void RunPressAnimation()
    {
        _originalLocation = transform.localPosition;
        //Debug.Log(_originalLocation);
        //_targetLocation = new Vector3(_originalLocation.x + _zMovement, _originalLocation.y, _originalLocation.z);
        //transform.DOLocalMove(_targetLocation, _moveDuration).SetEase(_moveEase)
            //.OnComplete(() => transform.DOLocalMove(_originalLocation, _moveDuration).SetEase(_moveEase));
        transform.DOLocalMoveZ(_originalLocation.z - _zMovement, _moveDuration, false)
            .OnComplete(() => transform.DOLocalMoveZ(_originalLocation.z, _moveDuration, false).SetEase(_moveEase));
        //transform.DOMove(_originalLocation, _moveDuration).SetEase(_moveEase);
    }
}
