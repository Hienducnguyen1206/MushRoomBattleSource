using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class EffectPoolManager : MonoBehaviour
{
    public static EffectPoolManager Instance { get; private set; }

    [SerializeField]
    private List<EffectType> effectTypes;

    private Dictionary<EffectType, ObjectPool<GameObject>> effectPools;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        effectPools = new Dictionary<EffectType, ObjectPool<GameObject>>();

        foreach (var effectType in effectTypes)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(effectType.EffectPrefab),
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: false,
                defaultCapacity: effectType.defaultCapacity,
                maxSize: effectType.maxSize
            );

            effectPools[effectType] = pool;
        }
    }

    public GameObject GetEffect(EffectType effectType)
    {
        if (effectPools.TryGetValue(effectType, out var pool))
        {
            return pool.Get();
        }
        else
        {
            Debug.LogError($"Effect type '{effectType.effectName}' not found.");
            return null;
        }
    }

    public void ReturnEffectToPool(GameObject effect, EffectType effectType)
    {
        if (effectPools.TryGetValue(effectType, out var pool))
        {
            pool.Release(effect);
        }
        else
        {
            Debug.LogError($"Effect type '{effectType.effectName}' not found.");
        }
    }
}
