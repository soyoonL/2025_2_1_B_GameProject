using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동설정")]
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    public float rotationSpeed = 10.0f;

    [Header("공격설정")]
    public float attackDuration = 0.8f;
    public bool canMoveWhileAttacking = false;

    [Header("커포넌트")]
    public Animator animator;

    private CharacterController controller;
    private Camera playerCamera;

    private float currentSpeed;
    private bool isAttacking = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    
    void Update()
    {
        HandleMovement();
        UpdateAnimator();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * v + cameraRight * h;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }

           controller.Move(moveDirection*currentSpeed*Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed*Time.deltaTime);
        }
        else
        {
            currentSpeed = 0;
        }
    }

    void UpdateAnimator()
    {
        float animatorSpeed = Mathf.Clamp01(currentSpeed / runSpeed);
        animator.SetFloat("speed",animatorSpeed);
    }
    
}
