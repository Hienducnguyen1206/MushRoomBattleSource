using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{   public float MaxX, MaxY;
    public int n;
    float x, y;
    void Start()
    {     
        StartCoroutine(Spawnmonstertogame(n) );
    }
  
    void Update()
    {
        
    }

    public IEnumerator Spawnmonstertogame(int n)
    { 
        for (int i = 0; i < n; i++)
        {  
            x = Random.Range(-MaxX, MaxX);  y = Random.Range(-MaxY, MaxY);
            Vector3 position = new Vector3(x, y, 0);
            ObjectPooler.Instance.spawnFromPool("Monster", position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
