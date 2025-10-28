using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Player;

[RequireComponent(typeof(Animator))]
public class CameraAnimator : MonoBehaviour
{
    [SerializeField] private PlayerService _playerService;
    [SerializeField] private PlayerConditions _playerConditions; // временно
    private Animator _animator;

    private void OnEnable()
    {
        _playerConditions.OnHighFall += Drag;
    }

    private void OnDisable()
    {
        _playerConditions.OnHighFall -= Drag;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Drag()
    {
        _animator.SetTrigger("Drag");
    }
}
