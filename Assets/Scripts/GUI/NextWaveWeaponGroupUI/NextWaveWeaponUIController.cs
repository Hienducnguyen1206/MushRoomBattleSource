using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveWeaponUIController : Singleton<NextWaveWeaponUIController>
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
    Color baseColor = new Color(1, 1, 1, 1);
    public bool WeaponSlotAreFull = false;
    public void SetFirstWeapon(string weaponName)
    {

        Image weaponImage = WeaponSlot[0].transform.GetChild(0).gameObject.GetComponent<Image>();

        if (weaponImage.sprite != null)
        {
            weaponImage.sprite = null;
        }


        switch (weaponName)
        {
            case "AK47":
                weaponImage.sprite = Rift;
                weaponImage.color = baseColor;
                break;
            case "Sniper":
                weaponImage.sprite = Sniper;
                weaponImage.color = baseColor;
                break;
            case "Shotgun":
                weaponImage.sprite = Shotgun;
                weaponImage.color = baseColor;
                break;
            case "LaserGun":
                weaponImage.sprite = Laser;
                weaponImage.color = baseColor;
                break;
            case "Bazooka":
                weaponImage.sprite = Bazooka;
                weaponImage.color = baseColor;
                break;
            case "ThunderStaff":
                weaponImage.sprite = ThunderStaff;
                weaponImage.color = baseColor;
                break;
            case "DarkStaff":
                weaponImage.sprite = DarkStaff;
                weaponImage.color = baseColor;

                break;
            case "FireStaff":
                weaponImage.sprite = FireStaff;
                weaponImage.color = baseColor;

                break;
            case "WaterStaff":
                weaponImage.sprite = WaterStaff;
                weaponImage.color = baseColor;

                break;
            case "NormalSword":
                weaponImage.sprite = NormalSword;
                weaponImage.color = baseColor;
                break;
            default:
                Debug.LogWarning("Unknown weapon name: " + weaponName);
                break;
        }
    }


    public void SetWeapon(string weaponName)
    {
        for (int i = 0; i < WeaponSlot.Count; i++)
        {
            Image weaponImage = WeaponSlot[i].transform.GetChild(0).gameObject.GetComponent<Image>();
            if (weaponImage.sprite != null)
            {
                continue;
            }
            else
            {          
                switch (weaponName)
                {
                    case "AK47":
                        weaponImage.sprite = Rift;
                        weaponImage.color = baseColor;
                        break;
                    case "Sniper":
                        weaponImage.sprite = Sniper;
                        weaponImage.color = baseColor;
                        break;
                    case "Shotgun":
                        weaponImage.sprite = Shotgun;
                        weaponImage.color = baseColor;
                        break;
                    case "LaserGun":
                        weaponImage.sprite = Laser;
                        weaponImage.color = baseColor;
                        break;
                    case "Bazooka":
                        weaponImage.sprite = Bazooka;
                        weaponImage.color = baseColor;
                        break;
                    case "ThunderStaff":
                        weaponImage.sprite = ThunderStaff;
                        weaponImage.color = baseColor;
                        break;
                    case "DarkStaff":
                        weaponImage.sprite = DarkStaff;
                        weaponImage.color = baseColor;

                        break;
                    case "FireStaff":
                        weaponImage.sprite = FireStaff;
                        weaponImage.color = baseColor;

                        break;
                    case "WaterStaff":
                        weaponImage.sprite = WaterStaff;
                        weaponImage.color = baseColor;

                        break;
                    case "NormalSword":
                        weaponImage.sprite = NormalSword;
                        weaponImage.color = baseColor;
                        break;
                    default:
                        Debug.LogWarning("Unknown weapon name: " + weaponName);
                        break;
                }
                return;

            }

        }
        WeaponSlotAreFull = true;
        Debug.Log("Weapon Slot is full");
    }

    public void RemoveWeapon(int ItemSlot)
    {
        SpriteRenderer weaponImage = WeaponSlot[ItemSlot].GetComponentInChildren<SpriteRenderer>();
        weaponImage.sprite = null;
    }

}
