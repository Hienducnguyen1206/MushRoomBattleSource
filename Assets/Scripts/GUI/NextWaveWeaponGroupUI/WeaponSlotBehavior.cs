using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlotBehavior : MonoBehaviour, IPointerClickHandler
{
    Button button;
    Color baseColor = new Color(0, 0, 0, 0);

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => UnEquipWeapon());

    }

    public void UnEquipWeapon()
    {
        Image weaponImage = gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Image>();
        

        if (weaponImage.sprite == null) return;
        string weaponName = weaponImage.sprite.name;
        WeaponInventoryController.Instance.AddItemToStore(weaponName);
        weaponImage.sprite = null;
        weaponImage.color = baseColor;
        Debug.Log(weaponName);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject weaponItem = gameObject.transform.parent.gameObject;


        int index = weaponItem.transform.GetSiblingIndex();
        
           GameController.Instance.UnEquipAWeapon(index);
           WeaponUIController.Instance.RemoveWeapon(index);

    }
}
