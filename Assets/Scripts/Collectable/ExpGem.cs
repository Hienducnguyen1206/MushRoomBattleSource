using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpGem : MonoBehaviour,ICollectable
{
    public Transform ExpBarUI;
    public float moveSpeed;
    public static event Action OnCollectedExp;
    Rigidbody2D rb;

    public CollectableType expGem;

    void OnEnable()
    {
        GameController.GetAllCollectable += Collected;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.0f;
    }

    void OnDisable()
    {
        GameController.GetAllCollectable -= Collected;
    }

    void Start()
    {
        ExpBarUI = GameObject.FindGameObjectWithTag("ExpBar").transform;
    }

   
    void Update()
    {
        
    }

    public void Collected()
    {
        StartCoroutine(MoveTowardsUI());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collected();
            rb.gravityScale = 0f;
        }
    }

    IEnumerator MoveTowardsUI()
    {
        while (true)
        {
            Vector3 targetPosition = ExpBarUI.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                OnCollectedExp?.Invoke();
                break;
            }

            yield return null;
        }


        CollectablePoolManager.Instance.ReturnCollectableToPool(gameObject, expGem);
    }
}
