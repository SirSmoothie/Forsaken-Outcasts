using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickupModule : NetworkBehaviour, IPickupable
{
    public int PickupItemIndex;
    private Transform ParentTransform;
    private IPickupParent PickupParent;
    private FollowTransform followTransform;

    private void Start()
    {
        followTransform = transform.GetComponent<FollowTransform>();
    }

    public void Pickup(GameObject WhoIsPickingItUp, GameObject WhereToParentItOn)
    {
        //GetComponent<Collider>().enabled = false;
        //GetComponent<Rigidbody>().isKinematic = true;
        //FollowTransform target = transform.GetComponent<FollowTransform>();
        //target.SetTargetTransform(WhereToParentItOn.transform);
        //^ old pickup method
        
        //WhoIsPickingItUp.GetComponent<CharacterModel>().SpawnItemInHand(PickupItemIndex);
        //NetworkObject.gameObject.SetActive(false);
        PickupModule thisItem = transform.GetComponent<PickupModule>();
        IPickupParent WhereToParent = WhereToParentItOn.GetComponent<IPickupParent>();
        PickUpMuliplayer.Instance.SpawnItemInPlayerHands(thisItem, WhereToParent);
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

    public void SetItemObjectParent(IPickupParent parent)
    {
        SetItemObjectParentServerRpc(parent.GetNetworkObject());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetItemObjectParentServerRpc(NetworkObjectReference PickupParentNetwork)
    {
        SetItemObjectParentClientRpc(PickupParentNetwork);
    }

    [ClientRpc]
    private void SetItemObjectParentClientRpc(NetworkObjectReference PickupParentNetwork)
    {
        PickupParentNetwork.TryGet(out NetworkObject ItemObjectParentNetworkObject);
        IPickupParent PickupParent = ItemObjectParentNetworkObject.GetComponent<IPickupParent>();

       // if (this.ItemParent != null)
        {
            //this.ItemParent.ClearItemObject;
        }

        this.PickupParent = PickupParent;

        PickupParent.SetItemObject(this);

        followTransform.SetTargetTransform(PickupParent.GetItemObjectFollowTransform());
    }
}
