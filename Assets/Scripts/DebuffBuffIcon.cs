using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffBuffIcon : MonoBehaviour
{
    [System.Serializable]
    public class StatusEffect
    {
        public string name;
        public Sprite image;

        public StatusEffect(string name, Sprite image)
        {
            this.name = name;
            this.image = image;
        }
    }

    public static DebuffBuffIcon Instance { get; private set; }
    public List<StatusEffect> effects;

    public Dictionary<string, Sprite> StatusIconDictionary = new Dictionary<string, Sprite>();

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

   
    void Start()
    {
        foreach (var effect in effects)
        {
            if (!StatusIconDictionary.ContainsKey(effect.name))
            {
                StatusIconDictionary.Add(effect.name, effect.image);
            }
            else
            {
                Debug.LogWarning("Effect " + effect.name + " already exists in StatusIconDictionary.");
            }
        }
    }

   
    void Update()
    {

    }
}
