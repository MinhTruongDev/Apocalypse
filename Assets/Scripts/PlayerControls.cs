using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    //PARAMETERS    
    [SerializeField] float movementSpeed = 15f;
    [SerializeField] float xRange = 13f;
    [SerializeField] float yRange = 8f;
    [SerializeField] GameObject[] lasers;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -15f;
    //PROPERTIES
    public float HorizontalThrow
    {
        get { return horizontalThrow; }
        set { horizontalThrow = value; }
    }
    public float VerticalThrow
    {
        get { return verticalThrow; }
        set { verticalThrow = value; }
    }

    //CACHE - references for readability or speed
    //STATE - private instance (member) variables
    private float horizontalThrow, verticalThrow, newXPos, newYPos, xOffset, yOffset, clampedXPos, clampedYPos;


    //PUBLIC METHOD    
    
    public void ProcessShooting(float fireInput)
    {
        if (fireInput > 0.5)
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }    

    public void RotatePlayerShip(float horizontalThrow, float verticalThrow)
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    public void MovePlayerShip(float horizontalThrow, float verticalThrow)
    {
        xOffset = horizontalThrow * Time.deltaTime * movementSpeed;
        yOffset = verticalThrow * Time.deltaTime * movementSpeed;

        newXPos = transform.localPosition.x + xOffset;
        newYPos = transform.localPosition.y + yOffset;

        clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }


    //PRIVATE METHOD    
    private void SetLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isActive;

            
        }
    }

   

}
