using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenController : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 1.0f;
    [SerializeField] private float _zMovement = 0.04f;
    [SerializeField] private Ease _moveEase = Ease.Linear;
    private Vector3 _targetLocation;
    private Vector3 _originalLocation;

    public void RunPressAnimation()
    {
        _originalLocation = transform.position;
        Debug.Log(_originalLocation);
        _targetLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z - _zMovement);
        transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase)
            .OnComplete(() => transform.DOMove(_originalLocation, _moveDuration).SetEase(_moveEase));
        //transform.DOMove(_originalLocation, _moveDuration).SetEase(_moveEase);
    }
}
