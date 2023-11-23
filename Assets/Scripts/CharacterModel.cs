using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterModel : NetworkBehaviour
{
    private Vector2 movementDirection;
    public Rigidbody rb;
    public Transform Camera;
    private Quaternion viewRoation;
    public float MaxCamHeight = 60;
    public float MinCamHeight = -60;
    public float cameraLookSpeed = 1;
    public float speed;
    private bool onGround;
    public float jumpHeight;
    public float onGroundDrag;

    private void Start()
    {
        if (!IsOwner) return;
        viewRoation = Camera.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    { 
        if (!IsOwner) return;
        if (!onGround)
        {
            //We want to move in air
            //rb.drag = 0f;
        }
        else
        {
            rb.drag = onGroundDrag;
        }
        
        
        
       Vector3 movementDirectionFinal = new Vector3(movementDirection.x, 0, movementDirection.y);
       rb.AddRelativeForce(movementDirectionFinal * speed, ForceMode.Acceleration);

       viewRoation.x = Mathf.Clamp(viewRoation.x, MinCamHeight, MaxCamHeight);
       Camera.transform.localRotation = Quaternion.Euler(viewRoation.x, 0, viewRoation.z);
       transform.localRotation = Quaternion.Euler(0, viewRoation.y, viewRoation.z);
    }

    public void MoveDirection(Vector2 Direction)
    {
        movementDirection = Direction;
    }

    public void CamDirection(Vector2 CamDirection)
    {
        viewRoation.x += -CamDirection.y * cameraLookSpeed;
        viewRoation.y += CamDirection.x * cameraLookSpeed;
    }

    public void Jump()
    {
        if (!onGround)
            return;

        //rb.drag = 0f;
        rb.AddForce(0, jumpHeight, 0, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        onGround = true;
    }

    private void OnCollisionExit(Collision other)
    {
        onGround = false;
    }
    
    private void OnCollisionStay(Collision other)
    {
        onGround = true;
    }
}
