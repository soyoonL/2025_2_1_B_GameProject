using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Ÿ�ټ���")]
    public Transform target;

    [Header("ī�޶� �Ÿ� ����")]
    public float distance = 8.0f;
    public float height = 5.0f;

    [Header("���콺 ����")]
    public float mouseSensitivity = 2.0f;
    public float minVecticalAngle = -30.0f;
    public float maxVecticalAngle = 60.0f;

    [Header("�ε巯�� ����")]
    public float positionSmoothTime = 0.2f;
    public float rotationSmoothTime = 0.1f;

    private float hAngle = 0.0f;
    private float vAngle= 0.0f;

    private Vector3 currentVelocity;
    private Vector3 currentPosition;
    private Quaternion currentRotation;

    void Start()
    {
        if(target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null )
                target = player.transform;
        }
        currentPosition = transform.position;
        currentRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { ToggleCursor(); }
    }

    void LateUpdate()
    {
        if (target == null) return;
        HandleMouseInput();
        UpdateCameraSmooth();
    }

    void HandleMouseInput()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        float mouseX= Input.GetAxis("Mouse X")*mouseSensitivity;
        float mouseY= Input.GetAxis("Mouse Y") * mouseSensitivity;

        hAngle += mouseX;
        vAngle += mouseY;

        vAngle = Mathf.Clamp(vAngle, minVecticalAngle, maxVecticalAngle);
    }

    void UpdateCameraSmooth()
    {
        Quaternion rotation = Quaternion.Euler(vAngle, hAngle, 0);
        Vector3 rotateOffset = rotation * new Vector3(0, height, -distance);
        Vector3 targetPosition = target.position + rotateOffset;

        Vector3 looktarget = target.position + Vector3.up * height;
        Quaternion targetRotation = Quaternion.LookRotation(looktarget - targetPosition);

        currentPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, positionSmoothTime);
        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime / rotationSmoothTime);

        transform.position = currentPosition;
        transform.rotation = currentRotation;

    }

    void ToggleCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
