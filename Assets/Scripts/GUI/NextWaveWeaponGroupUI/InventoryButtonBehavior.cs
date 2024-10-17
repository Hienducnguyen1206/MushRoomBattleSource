using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButtonBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string weaponName;
    Button button;




    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => NextWaveWeaponUIController.Instance.SetWeapon(weaponName));
        button.onClick.AddListener(() => WeaponUIController.Instance.SetWeapon(weaponName));
        button.onClick.AddListener(()=> GameController.Instance.SetWeapon(weaponName));
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject weaponItem = gameObject.transform.parent.gameObject;

    
        int index = weaponItem.transform.GetSiblingIndex();
        if (!NextWaveWeaponUIController.Instance.WeaponSlotAreFull)
        {
            WeaponInventoryController.Instance.RemoveItemFromStore(index);
        }
        
    }


}
