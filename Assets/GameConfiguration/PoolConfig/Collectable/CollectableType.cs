using UnityEngine;

[CreateAssetMenu(fileName = "CollectableType", menuName = "Pool/CollectableType", order = 1)]
public class CollectableType : ScriptableObject
{
    public string collectableName;
    public GameObject collectablePrefab;
    public int defaultCapacity = 10;
    public int maxSize = 200;
}
