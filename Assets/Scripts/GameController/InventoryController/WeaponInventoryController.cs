using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryController : Singleton<WeaponInventoryController>
{   
    List<GameObject> ItemStored = new List<GameObject>();
    

    [SerializeField] GameObject Ak47ItemPrefab;
    [SerializeField] GameObject SniperItemPrefab;
    [SerializeField] GameObject ShotgunItemPrefab;
    [SerializeField] GameObject BazookaItemPrefab;
    [SerializeField] GameObject LaserItemPrefab;

    [SerializeField] GameObject ThunderStaffItemPrefab;
    [SerializeField] GameObject FireStaffItemPrefab;
    [SerializeField] GameObject WaterStaffItemPrefab;
    [SerializeField] GameObject DarkStaffItemPrefab;

    [SerializeField] GameObject NormalSwordItemPrefab;


    // Start is called before the first frame update
    void Start()
    {
       


    }

    private void OnEnable()
    {

        DisplayStored();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemToStore(string name)
    {
        switch(name)
        {
             case "AK47":
                ItemStored.Add(Ak47ItemPrefab);
                break;
            case "Sniper":
                ItemStored.Add(SniperItemPrefab);
                break;
            case "Shotgun":
                ItemStored.Add(ShotgunItemPrefab);
                break;
            case "LaserGun":
                ItemStored.Add(LaserItemPrefab);
                break;
            case "Bazooka":
                ItemStored.Add(BazookaItemPrefab);
                break;
            case "ThunderStaff":
                ItemStored.Add(ThunderStaffItemPrefab);
                break;
            case "DarkStaff":
                ItemStored.Add(DarkStaffItemPrefab);

                break;
            case "FireStaff":
                ItemStored.Add(FireStaffItemPrefab);

                break;
            case "WaterStaff":
                ItemStored.Add(WaterStaffItemPrefab);

                break;
            case "NormalSword":
                ItemStored.Add(NormalSwordItemPrefab);
                break;
            default:
                Debug.LogWarning("Unknown weapon name: " + name);
                break;
        
        }
        DisplayStored();
    }

    public void RemoveItemFromStore(int index) 
    { 
        ItemStored.RemoveAt(index);
        DisplayStored();
    }

    public void DisplayStored()
    {
        OpenChestSystem.Instance.ClearAllChildren(transform.GetChild(0).gameObject);
        foreach (GameObject item in ItemStored)
        {
            GameObject weapon = Instantiate(item,transform.position,Quaternion.identity);
            weapon.transform.SetParent(transform.GetChild(0) ,false);
        }
    }



    
}
