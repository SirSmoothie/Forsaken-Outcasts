using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public Vector2 movementDirection;
    public Rigidbody rb;
    public float speed;
    private void FixedUpdate()
    {
        Vector3 movementDirectionFinal = new Vector3(movementDirection.x, 0, movementDirection.y);
        rb.AddForce(movementDirectionFinal * speed, ForceMode.Acceleration);
    }

    public void MoveDirection(Vector2 Direction)
    {
        movementDirection = Direction;
    }
}
