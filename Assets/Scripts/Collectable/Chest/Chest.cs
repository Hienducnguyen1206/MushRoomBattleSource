using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Chest: MonoBehaviour, ICollectable
{
    public Transform ChestUI;
    public float moveSpeed;
    public static event Action OnCollectedNormalChest;
    public static event Action OnCollectedRareChest;
    public static event Action OnCollectedEpicChest;
    public static event Action OnCollectedLegendaryChest;

    Rigidbody2D rb;
    public ChestTypeEnum typeChest;
    

    public CollectableType NormalChest;
    public CollectableType RareChest;
    public CollectableType EpicChest;
    public CollectableType LegendaryChest;

    private CollectableType ChestType;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        GameController.GetAllCollectable += Collected;
    }

    void OnDisable()
    {
        GameController.GetAllCollectable -= Collected;
    }
    void Start()
    {
        switch (typeChest)
        {
            case ChestTypeEnum.NORMALCHEST:
                {
                    ChestUI = GameObject.FindGameObjectWithTag("NormalChestUI").transform;
                    ChestType = NormalChest;
                    break;
                }
            case ChestTypeEnum.RARECHEST:
                {
                    ChestUI = GameObject.FindGameObjectWithTag("RareChestUI").transform;
                    ChestType = RareChest;
                    break;
                }
            case ChestTypeEnum.EPICCHEST:
                {
                    ChestUI = GameObject.FindGameObjectWithTag("EpicChestUI").transform;
                    ChestType = EpicChest;
                    break;
                }
            case ChestTypeEnum.LEGENDARYCHEST:
                {
                    ChestUI = GameObject.FindGameObjectWithTag("LegendaryChestUI").transform;
                    ChestType = LegendaryChest;
                    break;
                }
            default:
                {
                    break;
                }
        }



        
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
            Vector3 targetPosition = ChestUI.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                switch (typeChest)
                {
                    case ChestTypeEnum.NORMALCHEST:
                        {
                            OnCollectedNormalChest.Invoke();
                            break;
                        }
                    case ChestTypeEnum.RARECHEST:
                        {
                            OnCollectedRareChest.Invoke();
                            break;
                        }
                    case ChestTypeEnum.EPICCHEST:
                        {
                            OnCollectedEpicChest.Invoke();
                            break;
                        }
                    case ChestTypeEnum.LEGENDARYCHEST:
                        {
                            OnCollectedLegendaryChest.Invoke();
                            break;
                        }
                }

                break;
            }

            yield return null;
        }


        CollectablePoolManager.Instance.ReturnCollectableToPool(gameObject, ChestType);
    }





}
