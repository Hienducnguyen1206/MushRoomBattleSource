using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawn : MonoBehaviour
{ 
    [SerializeField] MonsterData monsterData;
    private MonsterHealth monsterHealth;
    public CollectableType Coin;
    public CollectableType ExpGem;
    public CollectableType NormalChest;
    public CollectableType RareChest;
    public CollectableType EpicChest;
    public CollectableType LegendaryChest;

    private void OnEnable()
    {
        monsterHealth = GetComponent<MonsterHealth>();
        monsterHealth.MonsterIsDead += SpawnCoin;
        monsterHealth.MonsterIsDead += SpawnExp;
        monsterHealth.MonsterIsDead += SpawmChest;
    }

    private void OnDisable()
    {
        monsterHealth.MonsterIsDead -= SpawnCoin;
        monsterHealth.MonsterIsDead -= SpawnExp;
        monsterHealth.MonsterIsDead -= SpawmChest;
    }



    void SpawnCoin()
    {
       

        int numbercoin = Random.Range(monsterData.MinCoin, monsterData.MaxCoin +1 );
    
        for (int i = 0; i < numbercoin; i++)
        {
            GameObject coin = CollectablePoolManager.Instance.GetCollectable(Coin);
            coin.transform.position = transform.position;
            coin.transform.rotation = Quaternion.identity;
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.2f)) * 3.5f;
            rb.AddForce(force, ForceMode2D.Impulse);
            float Stopposition = Random.Range(transform.position.y - 0.5f,transform.position.y + 0.4f);
            StartCoroutine(StopDropdown(Stopposition, rb, coin));
        }
    }

    void SpawnExp()
    {
      
        for (int i = 0; i < monsterData.Exp; i++)
        {
           
            GameObject expGem = CollectablePoolManager.Instance.GetCollectable(ExpGem);
            expGem.transform.position = transform.position;
            expGem.transform.rotation = Quaternion.identity;
            Rigidbody2D gemrb = expGem.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.2f)) * 3.5f;
            gemrb.AddForce(force, ForceMode2D.Impulse);
            float Stopposition = Random.Range(this.transform.position.y, this.transform.position.y + 0.1f);
            StartCoroutine(StopDropdown(Stopposition, gemrb, expGem));
        }
    }


    void SpawmChest()
    {
        bool spawnNormalChest = Services.Chance(monsterData.percentToSpawnNormalChest + monsterData.percentToSpawnNormalChest * PlayerCurrentStats.Instance.currentPlayerLuck/100);
        bool spawnRareChest = Services.Chance(monsterData.percentToSpawnRareChest + monsterData.percentToSpawnRareChest * PlayerCurrentStats.Instance.currentPlayerLuck/100);
        bool spawnEpicChest = Services.Chance(monsterData.percentToSpawnEpicChest + monsterData.percentToSpawnRareChest * PlayerCurrentStats.Instance.currentPlayerLuck / 100);
        bool spawnLegendaryChest = Services.Chance(monsterData.percentToSpawnLegendaryChest + monsterData.percentToSpawnRareChest * PlayerCurrentStats.Instance.currentPlayerLuck / 100);

        if (spawnNormalChest)
        {
            GameObject NormalChestToSpawn = CollectablePoolManager.Instance.GetCollectable(NormalChest);
            NormalChestToSpawn.transform.position = transform.position;
            NormalChestToSpawn.transform.rotation = Quaternion.identity;
            Rigidbody2D rb = NormalChestToSpawn.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.2f)) * 3.5f;
            rb.AddForce(force, ForceMode2D.Impulse);
            float Stopposition = Random.Range(this.transform.position.y, this.transform.position.y + 0.1f);
            StartCoroutine(StopDropdown(Stopposition, rb, NormalChestToSpawn));
        }

        if (spawnRareChest)
        {
            GameObject RareChestToSpawn = CollectablePoolManager.Instance.GetCollectable(RareChest);
            RareChestToSpawn.transform.position = transform.position;
            RareChestToSpawn.transform.rotation = Quaternion.identity;
            Rigidbody2D rb = RareChestToSpawn.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.2f)) * 3.5f;
            rb.AddForce(force, ForceMode2D.Impulse);
            float Stopposition = Random.Range(this.transform.position.y, this.transform.position.y + 0.1f);
            StartCoroutine(StopDropdown(Stopposition, rb, RareChestToSpawn));
        }

        if (spawnEpicChest)
        {
            GameObject EpicChestToSpawn = CollectablePoolManager.Instance.GetCollectable(EpicChest);
            EpicChestToSpawn.transform.position = transform.position;
            EpicChestToSpawn.transform.rotation = Quaternion.identity;
            Rigidbody2D rb = EpicChestToSpawn.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.2f)) * 3.5f;
            rb.AddForce(force, ForceMode2D.Impulse);
            float Stopposition = Random.Range(this.transform.position.y, this.transform.position.y + 0.1f);
            StartCoroutine(StopDropdown(Stopposition, rb, EpicChestToSpawn));
        }

        if (spawnLegendaryChest)
        {
            GameObject LegendaryChestToSpawn = CollectablePoolManager.Instance.GetCollectable(LegendaryChest);
            LegendaryChestToSpawn.transform.position = transform.position;
            LegendaryChestToSpawn.transform.rotation = Quaternion.identity;
            Rigidbody2D rb = LegendaryChestToSpawn.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.2f)) * 3.5f;
            rb.AddForce(force, ForceMode2D.Impulse);
            float Stopposition = Random.Range(this.transform.position.y, this.transform.position.y + 0.1f);
            StartCoroutine(StopDropdown(Stopposition, rb, LegendaryChestToSpawn));
        }

    }

    IEnumerator StopDropdown(float StopPosition, Rigidbody2D rb, GameObject collectable)
    {
        while (collectable.transform.position.y > StopPosition)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }

}

