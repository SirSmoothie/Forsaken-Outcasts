using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    public void Pickup(GameObject WhoIsPickingItUp, GameObject WhereToParentItOn);

    public void Drop();
}
