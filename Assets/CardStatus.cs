using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStatus : MonoBehaviour
{
    public bool Locked;
    public bool SoldOut;
    [SerializeField] GameObject LockIcon;
    [SerializeField] GameObject UnlockIcon;


    private void Start()
    {
        Locked = false;
        SoldOut = false;
    }


    public void LocktheCard()
    {
        Locked = true;
        Debug.Log("Lock");
    }

    public void UnLocktheCard()
    {
        Locked = false;
        Debug.Log("Unlock");
    }


}
