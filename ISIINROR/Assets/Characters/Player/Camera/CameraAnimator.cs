using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Player;

[RequireComponent(typeof(Animator))]
public class CameraAnimator : MonoBehaviour
{
    [SerializeField] private PlayerService _playerService;
    private Animator _animator;

    private void OnEnable()
    {
        if (!_playerService || !_playerService.Conditions)
        {
            PlayerConditions conditions = FindObjectOfType<PlayerConditions>();
            conditions.OnHighFall += Drag;
        }
        else
        {
            _playerService.Conditions.OnHighFall += Drag;
        }
    }

    private void OnDisable()
    {
        _playerService.Conditions.OnHighFall -= Drag;
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
