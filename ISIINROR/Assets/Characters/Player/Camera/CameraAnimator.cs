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
        _playerService.OnHighFall += Drag;
    }

    private void OnDisable()
    {
        _playerService.OnHighFall -= Drag;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Drag()
    {
        _animator.SetTrigger("Drag");
    }
}
