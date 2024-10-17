using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }

    [SerializeField]
    private List<BulletType> bulletTypes;

    private Dictionary<BulletType, ObjectPool<GameObject>> bulletPools;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bulletPools = new Dictionary<BulletType, ObjectPool<GameObject>>();

        foreach (var bulletType in bulletTypes)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(bulletType.bulletPrefab),
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: obj => obj.SetActive(false),
                collectionCheck: false,
                defaultCapacity: bulletType.defaultCapacity,
                maxSize: bulletType.maxSize
            );

            bulletPools[bulletType] = pool;
        }
    }

    public GameObject GetBullet(BulletType bulletType)
    {
        if (bulletPools.TryGetValue(bulletType, out var pool))
        {
            return pool.Get();
        }
        else
        {
            Debug.LogError($"Bullet type '{bulletType.bulletName}' not found.");
            return null;
        }
    }

    public void ReturnBulletToPool(GameObject bullet, BulletType bulletType)
    {
        if (bulletPools.TryGetValue(bulletType, out var pool))
        {
            pool.Release(bullet);
        }
        else
        {
            Debug.LogError($"Bullet type '{bulletType.bulletName}' not found.");
        }
    }
}
