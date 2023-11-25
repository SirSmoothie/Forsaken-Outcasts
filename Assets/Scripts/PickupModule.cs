using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickupModule : NetworkBehaviour, IPickupable
{
    
    public void Pickup(GameObject WhoIsPickingItUp, GameObject WhereToParentItOn)
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        FollowTransform target = transform.GetComponent<FollowTransform>();
        target.SetTargetTransform(WhereToParentItOn.transform);
    }

    public void Drop()
    {
        FollowTransform target = transform.GetComponent<FollowTransform>();
        target.SetTargetTransform(null);
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
