using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    public float playerSpeed;
    public float currentFov;
    public float desiredFov;
    const float zoomStep = 100.0f;
    Camera playerCamera;

    void Start()
    {
        currentFov = 70f;
        desiredFov = currentFov;
        playerCamera = Camera.main;
    }

    void CheckSpeed()
    {
        if (playerSpeed > 5f)
        {
            desiredFov = 80f;
        }
        else
        {
            desiredFov = 70f;
        }
    }

    void ProcessFOV()
    {
        currentFov = Mathf.MoveTowards(currentFov, desiredFov, zoomStep * Time.deltaTime);
    }

    void SetFOV()
    {
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = currentFov;
        }
    }

    void Update()
    {
        playerSpeed = GameObject.Find("First Person Controller").GetComponent<Rigidbody>().velocity.magnitude;

        CheckSpeed();
        ProcessFOV();
        SetFOV();
    }
}