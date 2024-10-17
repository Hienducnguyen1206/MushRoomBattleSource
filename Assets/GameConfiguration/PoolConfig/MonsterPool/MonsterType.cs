using UnityEngine;

[CreateAssetMenu(fileName = "MonsterType", menuName = "Pool/MonsterType", order = 2)]
public class MonsterType : ScriptableObject
{
    public string monsterName;
    public GameObject MonsterPrefab;
    public int defaultCapacity = 10;
    public int maxSize = 40;
}
