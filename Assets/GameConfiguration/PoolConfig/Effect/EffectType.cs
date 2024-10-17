using UnityEngine;

[CreateAssetMenu(fileName = "EffectType", menuName = "Pool/EffectType", order = 1)]
public class EffectType : ScriptableObject
{
    public string effectName;
    public GameObject EffectPrefab;
    public int defaultCapacity = 10;
    public int maxSize = 40;
}
