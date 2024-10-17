using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInformationButton : MonoBehaviour
{   Button button;
    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.Select();
    }
}
