using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickupModule : NetworkBehaviour, IPickupable
{
    public void Pickup(GameObject WhoIsPickingItUp, GameObject WhereToParentItOn)
    {
        transform.parent = WhereToParentItOn.transform;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = WhereToParentItOn.transform.position;
        transform.rotation = WhereToParentItOn.transform.rotation;
    }

    public void Drop()
    {
        transform.parent = null;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
