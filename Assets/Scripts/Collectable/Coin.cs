using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public Transform CoinUI;
    public float moveSpeed ;
    public static event Action OnCollectedCoin;
    Rigidbody2D rb ;

    public CollectableType coin;
    
    void OnEnable()
    {   rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.0f;
        GameController.GetAllCollectable += Collected ;
    }

    void OnDisable() {
        GameController.GetAllCollectable -= Collected;
    }
    void Start()
    {
        CoinUI = GameObject.FindGameObjectWithTag("Coins").transform;
    }

    void Update()
    {
        
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {        
            Collected();
        }
    }
    public void Collected()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(MoveTowardsUI());
        }
        
    }

    IEnumerator MoveTowardsUI()
    {  
        while (true)
        {
            Vector3 targetPosition = CoinUI.position; 
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

           
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                OnCollectedCoin?.Invoke();
                break;
            }

            yield return null; 
        }

       
        CollectablePoolManager.Instance.ReturnCollectableToPool(gameObject, coin);
    }

    

    
   
}
