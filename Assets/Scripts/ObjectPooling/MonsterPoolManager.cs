using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class MonsterPoolManager : MonoBehaviour
{
    public static MonsterPoolManager Instance { get; private set; }

    [SerializeField]
    private List<MonsterType> monsterTypes;

    private Dictionary<MonsterType, ObjectPool<GameObject>> monsterPools;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        monsterPools = new Dictionary<MonsterType, ObjectPool<GameObject>>();

        foreach (var monsterType in monsterTypes)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(monsterType.MonsterPrefab),
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: false,
                defaultCapacity: monsterType.defaultCapacity,
                maxSize: monsterType.maxSize
            );

            monsterPools[monsterType] = pool;
        }
    }

    public GameObject GetMonster(MonsterType monsterType)
    {
        if (monsterPools.TryGetValue(monsterType, out var pool))
        {
            return pool.Get();
        }
        else
        {
            Debug.LogError($"Monster type '{monsterType.monsterName}' not found.");
            return null;
        }
    }

    public void ReturnMonsterToPool(GameObject monster, MonsterType monsterType)
    {
        if (monsterPools.TryGetValue(monsterType, out var pool))
        {
            pool.Release(monster);
        }
        else
        {
            Debug.LogError($"Monster type '{monsterType.monsterName}' not found.");
        }
    }
}
