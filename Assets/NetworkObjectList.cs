using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/NetworkObjectList", order = 1)]
public class NetworkObjectList : ScriptableObject
{
    public GameObject[] ItemPrefabs;
}
