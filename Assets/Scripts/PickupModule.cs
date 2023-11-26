using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickupModule : NetworkBehaviour, IPickupable
{
    public int PickupItemIndex;
    
    public void Pickup(GameObject WhoIsPickingItUp, GameObject WhereToParentItOn)
    {
        //GetComponent<Collider>().enabled = false;
        //GetComponent<Rigidbody>().isKinematic = true;
        //FollowTransform target = transform.GetComponent<FollowTransform>();
        //target.SetTargetTransform(WhereToParentItOn.transform);
        //^ old pickup method
        
        WhoIsPickingItUp.GetComponent<CharacterModel>().SpawnItemInHand(PickupItemIndex);
        gameObject.SetActive(false);
    }

    public void Drop(GameObject WhoIsDroppingIt)
    {
        //FollowTransform target = transform.GetComponent<FollowTransform>();
        //target.SetTargetTransform(null);
        //GetComponent<Collider>().enabled = true;
        //GetComponent<Rigidbody>().isKinematic = false;
        //^ Old Drop Method
        
        WhoIsDroppingIt.GetComponent<CharacterModel>().SpawnItemOutOfHand(PickupItemIndex);
        Destroy(gameObject);
    }
    
}
