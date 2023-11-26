using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickUpMuliplayer : NetworkBehaviour
{
    public static PickUpMuliplayer Instance { get; private set; }

    [SerializeField] private NetworkObjectList _networkObjectList;
    private void Awake()
    {
        Instance = this;
    }

    public void SpawnItemInPlayerHands(PickupModule Pickup, IPickupParent PickupParent)
    {
        SpawnItemObjectServerRpc(GetItemObjectIndex(Pickup), PickupParent.GetNetworkObject());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnItemObjectServerRpc(int NetworkObjectListIndex, NetworkObjectReference PickupParentNetwork)
    {
        PickupModule Pickup = GetPickupObjectFromIndex(NetworkObjectListIndex);
        Transform ItemObjectTransform = Instantiate(Pickup.transform);

        NetworkObject ItemNetworkObject = ItemObjectTransform.GetComponent<NetworkObject>();
        ItemNetworkObject.Spawn(true);
        PickupModule pickupObject = ItemObjectTransform.GetComponent<PickupModule>();

        PickupParentNetwork.TryGet(out NetworkObject ItemObjectParentNetworkObject);
        IPickupParent PickupParent = ItemObjectParentNetworkObject.GetComponent<IPickupParent>();
        pickupObject.SetItemObjectParent(PickupParent);
    }

    private int GetItemObjectIndex(PickupModule pickup)
    {
        return _networkObjectList.PickupItemPrefabs.IndexOf(pickup) + 1;
    }

    private PickupModule GetPickupObjectFromIndex(int ObjectIndex)
    {
        return _networkObjectList.PickupItemPrefabs[ObjectIndex];
    }
}
