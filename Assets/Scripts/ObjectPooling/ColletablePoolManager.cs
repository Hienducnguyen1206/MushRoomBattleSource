using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class CollectablePoolManager:MonoBehaviour
{
    public static CollectablePoolManager Instance { get; private set; }

    [SerializeField]
    private List<CollectableType> collectableTypes;

    private Dictionary<CollectableType, ObjectPool<GameObject>> collectablePools;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        collectablePools = new Dictionary<CollectableType, ObjectPool<GameObject>>();

        foreach (var collectableType in collectableTypes)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(collectableType.collectablePrefab),
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: false,
                defaultCapacity: collectableType.defaultCapacity,
                maxSize: collectableType.maxSize
            );

            collectablePools[collectableType] = pool;
        }
    }

    public GameObject GetCollectable(CollectableType collectableType)
    {
        if (collectablePools.TryGetValue(collectableType, out var pool))
        {
            return pool.Get();
        }
        else
        {
            Debug.LogError($"Collectable type '{collectableType.collectableName}' not found.");
            return null;
        }
    }

    public void ReturnCollectableToPool(GameObject collectable, CollectableType collectableType)
    {
        if (collectablePools.TryGetValue(collectableType, out var pool))
        {
            pool.Release(collectable);
        }
        else
        {
            Debug.LogError($"Collectable type '{collectableType.collectableName}' not found.");
        }
    }
}
