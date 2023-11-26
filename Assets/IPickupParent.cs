using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public interface IPickupParent
{
    public NetworkObject GetNetworkObject();

    public void SetItemObject(PickupModule gameObject);

    public Transform GetItemObjectFollowTransform();
}
