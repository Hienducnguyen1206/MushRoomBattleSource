using UnityEngine;

[CreateAssetMenu(fileName = "BulletType", menuName = "Pool/BulletType", order = 1)]
public class BulletType : ScriptableObject
{
    public string bulletName;
    public GameObject bulletPrefab;
    public int defaultCapacity = 10;
    public int maxSize = 20;
}
