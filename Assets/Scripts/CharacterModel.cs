using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class CharacterModel : NetworkBehaviour, IPickupParent
{
    private Vector2 movementDirection;
    public Rigidbody rb;
    public Transform Camera;
    public GameObject HandsSlot;
    public bool holdingObject;
    private Quaternion viewRoation;
    public NetworkObjectList objectList;
    public float MaxCamHeight = 60;
    public float MinCamHeight = -60;
    public float cameraLookSpeed = 1;
    public float speed;
    private bool onGround;
    public float jumpHeight;
    public float onGroundDrag;
    public float playerReach;
    public Vector3 interactRayOffset = new Vector3(0,0.5f,0);
    public GameObject CurrentObject;

    private GameObject Whatyoupickedup;
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

    public void Interact()
    {
        RaycastHit hit = CheckWhatsInFront();
       
        
        if (holdingObject)
        {
            IPickupable pickup = Whatyoupickedup.transform.GetComponent<IPickupable>();
            pickup?.Drop(gameObject);
            holdingObject = false;
        }
        else
        {
            IPickupable pickup = hit.transform.GetComponent<IPickupable>();
            pickup?.Pickup(gameObject, gameObject);
            Whatyoupickedup = hit.transform.gameObject;
            holdingObject = true;
        }
    }
    
    public void SpawnItemInHand(int Value)
    {
        //var item = Instantiate(objectList.ItemPrefabs[Value], HandsSlot.transform.position, quaternion.identity, HandsSlot.transform);
        //item.GetComponent<NetworkObject>().Spawn();
        
        
    }
    
    public void SpawnItemOutOfHand(int Value)                                                                                                    
    {                                                                                                                                         
        //var itemdrop = Instantiate(objectList.ItemPrefabs[Value], HandsSlot.transform.position, quaternion.identity, null);
        //itemdrop.GetComponent<NetworkObject>().Spawn();
        //itemdrop.GetComponent<Rigidbody>().isKinematic = false;
    }                                                                                                                                         
    public RaycastHit CheckWhatsInFront()
    {
        // Check what's in front of me. TODO: Make it scan the area or something less precise
        RaycastHit hit;
        // Ray        ray = new Ray(transform.position + transform.TransformPoint(interactRayOffset), transform.forward);
        // NOTE: TransformPoint I THINK includes the main position, so you don't have to add world position to the final
        Vector3 transformPoint = transform.TransformPoint(interactRayOffset);
        // Debug.Log(transformPoint);
        Ray ray = new Ray(transformPoint, Camera.forward);

        Debug.DrawRay(ray.origin, ray.direction * playerReach, Color.green, 2f);

        // if (Physics.Raycast(ray, out hit, interactDistance))
        Physics.SphereCast(ray, 0.5f, out hit, playerReach);

        return hit;
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

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }

    public void SetItemObject(PickupModule gameObject)
    {
        CurrentObject = gameObject.gameObject;
    }

    public Transform GetItemObjectFollowTransform()
    {
        return HandsSlot.transform;
    }
}
