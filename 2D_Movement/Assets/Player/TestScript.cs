using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.2f;
    public float wallSlideSpeed = 1.5f;
    public float detachDelay = 0.3f;

    public GroundChecker groundchacker;

    private Rigidbody2D rb;
    private bool isTouchingWall;
    private bool isWallClinging;
    private float detachTimer;
    private float inputHorizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");

        CheckWall();

        if(!groundchacker._isGrounded && isTouchingWall && inputHorizontal != 0)
        {
            StartWallClinging();
        }
        else
        {
            StopWallClinging();
        }

        if(isWallClinging && Input.GetButtonDown("Jump"))
        {
            WallJump();
        }
    }

    private void WallJump()
    {

    }

    private void StartWallClinging()
    {

    }

    private void StopWallClinging()
    {

    }

    private void CheckWall()
    {
        
    }
}
