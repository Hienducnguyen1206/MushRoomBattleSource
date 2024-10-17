
using UnityEngine;
using UnityEngine.UI;


public class HoverWeaponIcons : MonoBehaviour
{
    [SerializeField] ScriptableObject Data;
    [SerializeField] string weaponName;
    Button button;
    


    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(ShowInformaion);
        button.onClick.AddListener(SetPlayerFirstWeapon);
        button.onClick.AddListener(SetFirstWeaponInInformationMenu);
        button.onClick.AddListener(() => NextWaveWeaponUIController.Instance.SetFirstWeapon(weaponName));
    }
    public void ShowInformaion()
    {
        WeaponInformaionPopUp.Instance.ShowPopUp(Data);
    }

    public void SetPlayerFirstWeapon()
    {
        GameController.Instance.SetFirstWeapon(weaponName);
       
    }

    public void SetFirstWeaponInInformationMenu()
    {
        WeaponUIController.Instance.SetFirstWeapon(weaponName);
    }

    


    
   
}
