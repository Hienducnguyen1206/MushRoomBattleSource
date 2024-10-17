using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponUIController : Singleton<WeaponUIController>
{
    [SerializeField] Sprite Rift;
    [SerializeField] Sprite Shotgun;
    [SerializeField] Sprite Laser;
    [SerializeField] Sprite Sniper;
    [SerializeField] Sprite Bazooka;

    [SerializeField] Sprite ThunderStaff;
    [SerializeField] Sprite WaterStaff;
    [SerializeField] Sprite FireStaff;
    [SerializeField] Sprite DarkStaff;

    [SerializeField] Sprite NormalSword;



    [SerializeField] List<GameObject> WeaponSlot;
    Vector3 staffScale = new Vector3(70, 70, 0);
    Vector3 GunScale = new Vector3(20, 20, 0);




    public void SetFirstWeapon(string weaponName)
    {
       
        SpriteRenderer weaponImage = WeaponSlot[0].GetComponentInChildren<SpriteRenderer>();

        if (weaponImage.sprite != null)
        {
            weaponImage.sprite = null;
        }

       
        switch (weaponName)
        {
            case "AK47":
                weaponImage.sprite = Rift;
                weaponImage.gameObject.transform.localScale = GunScale;
                break;
            case "Sniper":
                weaponImage.sprite = Sniper;
                weaponImage.gameObject.transform.localScale = GunScale;
                break;
            case "Shotgun":
                weaponImage.sprite = Shotgun;
                weaponImage.gameObject.transform.localScale = GunScale;
                break;
            case "LaserGun":
                weaponImage.sprite = Laser;
                weaponImage.gameObject.transform.localScale = GunScale;
                break;
            case "Bazooka":
                weaponImage.sprite = Bazooka;
                weaponImage.gameObject.transform.localScale = GunScale;
                break;
            case "ThunderStaff":
                weaponImage.sprite = ThunderStaff;
                weaponImage.gameObject.transform.localScale = staffScale;
                break;
            case "DarkStaff":
                weaponImage.sprite = DarkStaff;
                weaponImage.gameObject.transform.localScale = staffScale;
                break;
            case "FireStaff":
                weaponImage.sprite = FireStaff;
                weaponImage.gameObject.transform.localScale = staffScale;
                break;
            case "WaterStaff":
                weaponImage.sprite = WaterStaff;
                weaponImage.gameObject.transform.localScale = staffScale;
                break;
            case "NormalSword":
                weaponImage.sprite = NormalSword;
                weaponImage.gameObject.transform.localScale = staffScale;
                break;
            default:
                Debug.LogWarning("Unknown weapon name: " + weaponName);
                break;
        }
    }




    public void SetWeapon(string weaponName)
    {
        for(int i = 0; i < WeaponSlot.Count; i++)
        {
            SpriteRenderer weaponImage = WeaponSlot[i].GetComponentInChildren<SpriteRenderer>();
            if ( weaponImage.sprite != null)
            {
                continue;
            }
            else
            {
                switch (weaponName)
                {
                    case "AK47":
                        weaponImage.sprite = Rift;
                        break;
                    case "Sniper":
                        weaponImage.sprite = Sniper;
                        break;
                    case "Shotgun":
                        weaponImage.sprite = Shotgun;
                        break;
                    case "LaserGun":
                        weaponImage.sprite = Laser;
                        break;
                    case "Bazooka":
                        weaponImage.sprite = Bazooka;
                        break;
                    case "ThunderStaff":
                        weaponImage.sprite = ThunderStaff;
                        weaponImage.gameObject.transform.localScale = staffScale;
                        break;
                    case "DarkStaff":
                        weaponImage.sprite = DarkStaff;
                        weaponImage.gameObject.transform.localScale = staffScale;
                        break;
                    case "FireStaff":
                        weaponImage.sprite = FireStaff;
                        weaponImage.gameObject.transform.localScale = staffScale;
                        break;
                    case "WaterStaff":
                        weaponImage.sprite = WaterStaff;
                        weaponImage.gameObject.transform.localScale = staffScale;
                        break;
                    case "NormalSword":
                        weaponImage.sprite = NormalSword;
                        weaponImage.gameObject.transform.localScale = staffScale;
                        break;
                    default:
                        Debug.LogWarning("Unknown weapon name: " + weaponName);
                        break;
                }
                return;

            }

        }
        Debug.Log("Weapon Slot is full");
    }

    public void RemoveWeapon(int ItemSlot)
    {
        SpriteRenderer weaponImage = WeaponSlot[ItemSlot].GetComponentInChildren<SpriteRenderer>();
        weaponImage.sprite = null;
    }
}
