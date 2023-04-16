using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    //PARAMETERS
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] float movementSpeed = 15f;
    [SerializeField] float xRange = 13f;
    [SerializeField] float yRange = 8f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -15f;

    //CACHE - references for readability or speed
    //STATE - private instance (member) variables
    private float horizontalThrow, verticalThrow;


    //PUBLIC METHOD    
    void FixedUpdate()
    {
        InputHandler();
    }
    //PRIVATE METHOD
    private void InputHandler()
    {
        horizontalThrow = movement.ReadValue<Vector2>().x;
        verticalThrow = movement.ReadValue<Vector2>().y;

        MovePlayerShip(horizontalThrow, verticalThrow);
        RotatePlayerShip(horizontalThrow, verticalThrow);
        ProcessShooting();
        //horizontalThrow = Input.GetAxis("Horizontal");
        //verticalThrow = Input.GetAxis("Vertical");
    }

    private void ProcessShooting()
    {
        if (fire.ReadValue<float>() > 0.5)
        {
            Debug.Log("Shooting");
        }
        else
        {
            Debug.Log("Stop Shooting");
        }
    }

    private void RotatePlayerShip(float horizontalThrow, float verticalThrow)
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    private void MovePlayerShip(float horizontalThrow, float verticalThrow)
    {
        float newXPos, newYPos, xOffset, yOffset, clampedXPos, clampedYPos;

        xOffset = horizontalThrow * Time.deltaTime * movementSpeed;
        yOffset = verticalThrow * Time.deltaTime * movementSpeed;

        newXPos = transform.localPosition.x + xOffset;
        newYPos = transform.localPosition.y + yOffset;

        clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }
}
