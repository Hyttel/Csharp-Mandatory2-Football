using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4.0f;
    [SerializeField] private float shootingPower = 0.7f;
    private Transform transformBall;
    private Player scriptPlayer;
    private Animator animator;
    private Transform playerBallPosition;
    private Vector3 targetGoalPosition;
    private Vector3 ownGoalPosition;
    private Vector3[] attackTargetLocation = new Vector3[2];
    
    void Start()
    {
        transformBall = GameObject.Find("Ball").transform;
        scriptPlayer = GetComponent<Player>();
        animator = GetComponent<Animator>();
        playerBallPosition = transform.Find("BallPosition");
    }

    void Update()
    {
        MoveToBall();
    }

// Hvorfor løber de ikke?
    private void MoveToBall()
    {
        Vector3 lookAtPosition = transformBall.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
        transform.position = Vector3.MoveTowards(transform.position, transformBall.position, movementSpeed * Time.deltaTime);
        Vector3 movedirection = transformBall.position - playerBallPosition.position;
        Vector3 moveSpeed = new Vector3(movedirection.normalized.x * movementSpeed * Time.deltaTime, 0, movedirection.normalized.z * movementSpeed * Time.deltaTime);
        transform.position += moveSpeed;
        animator.SetFloat("Speed", moveSpeed.magnitude * 200);
        animator.SetFloat("MotionSpeed", 1);
    }
}