using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public static LevelDesign Instance;
    private void Awake()
    {
        Instance = this;
    }

    [System.Serializable]
    public class Playerlevel {
        public int level;
        public int ExperienceRequired;
    }

    public List<Playerlevel> m_Players;

}
