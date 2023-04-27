using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //PARAMETERS
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    //CACHE - references for readability or speed
    PlayerControls playerControls;
    //STATE - private instance (member) variables
    //PUBLIC METHOD
    void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
    }
    void Update()
    {
        InputController();    
    }
    //PRIVATE METHOD 
    private void InputController()
    {
        playerControls.HorizontalThrow = movement.ReadValue<Vector2>().x;
        playerControls.VerticalThrow = movement.ReadValue<Vector2>().y;        

        playerControls.MovePlayerShip(playerControls.HorizontalThrow, playerControls.VerticalThrow);
        playerControls.RotatePlayerShip(playerControls.HorizontalThrow, playerControls.VerticalThrow);
        playerControls.ProcessShooting(fire.ReadValue<float>());
        //horizontalThrow = Input.GetAxis("Horizontal");
        //verticalThrow = Input.GetAxis("Vertical");
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
