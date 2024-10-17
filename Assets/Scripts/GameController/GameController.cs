using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Threading;

[System.Serializable]
public class Monster
{
    public int number; 
    public string name;
    public float timeDelay;
    public MonsterType monsterType;
}

[System.Serializable]
public class Wave
{
    public int wave_id;
    public int wave_time;

    [SerializeField]
    public List<Monster> monsters = new List<Monster>();
}

public class GameController : Singleton<GameController>
{
   
    [SerializeField]
    public List<Wave> Waves = new List<Wave>();
    [SerializeField]
    public float MaxX, MaxY;
    [SerializeField] GameObject OpenChestMenu;

    public PolygonCollider2D SpawmZone;
    float x, y;
   public int currentWave = 0;

    

    // Kill all monster when wave end
    public static event Action KillAllMonster;
    // Get all Collectable when wave end 
    public static event Action GetAllCollectable;

    public TextMeshProUGUI WaveTimeText;
    public TextMeshProUGUI WaveNumberText;
    public bool isEndWave;

    

    [SerializeField] GameObject CharacterStartMenu;
    [SerializeField] GameObject StartGameMenu;
    [SerializeField] GameObject EndGameMenu;
    [SerializeField] GameObject WinGameMenu;
    [SerializeField] GameObject NextWaveGameMenu;
    [SerializeField] GameObject ShopMenu;



    public Vector3 staffRotation = new Vector3(0, 0, 90);
    

    [SerializeField] GameObject Ak47Prefab;
    [SerializeField] GameObject SniperPrefab;
    [SerializeField] GameObject ShotgunPrefab;
    [SerializeField] GameObject BazookaPrefab;
    [SerializeField] GameObject LaserGunPrefab;

    [SerializeField] GameObject ThunderStaffPrefab;
    [SerializeField] GameObject WaterStaffPrefab;
    [SerializeField] GameObject DarkStaffPrefab;
    [SerializeField] GameObject FireStaffPrefab;

    [SerializeField] GameObject NormalSwordPrefab;


    private void Start()
    {
        StartGameMenu.SetActive(true);
        AudioController.Instance.GetComponent<AudioSource>().Play();
    }

    public void StartFirstWave()
    {   
        StartWave(currentWave);
    }

    void StartWave(int wave_id)
    {
        StartCoroutine(StartWaveTime(Waves[currentWave].wave_time));
        isEndWave = false;
        foreach (var monster in Waves[wave_id].monsters)
        {
            StartCoroutine(SpawnMonsterFromList(monster));
        }
        PlayerHealth.Instance.StartRegenHP();
    }

    void EndWave(int wave_id)
    {
        PlayerHealth.Instance.StopRegenHP();
        LevelChestController.OnLevelUp();
        foreach (var monster in Waves[wave_id].monsters)
        {
            StopCoroutine(SpawnMonsterFromList(monster));
        }
        isEndWave = true;
        KillAllMonster?.Invoke();
        StartCoroutine(GetAllTreasure());
        currentWave += 1;
      
        OpenMenu();
       
    }

    void OpenMenu() {
        StartCoroutine(OpenChestMenuCorroutine());
    }
    public void NextWave() {
        if(currentWave < Waves.Count)
        {
            StartWave(currentWave);
        }
        else
        {
           WinGameMenu.SetActive(true);
        }
        
    }
    public void GameOver() { 
        EndGameMenu.SetActive(true);
        StopAllCoroutines();
    }

    public void OpenNextWaveMenu() {
        NextWaveGameMenu.SetActive(true) ;
    }

    public void OpenShopMenu()
    {
        ShopMenu.SetActive(true);
        ShopSystem.Instance.DisplayShopCard();
    }

    public void SetFirstWeapon(string weaponName)
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weapon = null;

       
        for (int i = 0; i < player.transform.childCount; i++)
        {
            
            if (player.transform.GetChild(i).childCount > 0)
            {
                GameObject currentWeapon = player.transform.GetChild(i).GetChild(0).gameObject;
                Destroy(currentWeapon); 
            }

          
            switch (weaponName)
            {
                case "AK47":
                    weapon = Instantiate(Ak47Prefab, player.transform.GetChild(i).position, Quaternion.identity);
                    break;
                case "Sniper":
                    weapon = Instantiate(SniperPrefab, player.transform.GetChild(i).position, Quaternion.identity);
                    break;
                case "Shotgun":
                    weapon = Instantiate(ShotgunPrefab, player.transform.GetChild(i).position, Quaternion.identity);
                    break;
                case "LaserGun":
                    weapon = Instantiate(LaserGunPrefab, player.transform.GetChild(i).position, Quaternion.identity);
                    break;
                case "Bazooka":
                    weapon = Instantiate(BazookaPrefab, player.transform.GetChild(i).position, Quaternion.identity);
                    break;
                case "ThunderStaff":
                    weapon = Instantiate(ThunderStaffPrefab, player.transform.GetChild(i).position, Quaternion.Euler(staffRotation));
                    break;
                case "DarkStaff":
                    weapon = Instantiate(DarkStaffPrefab, player.transform.GetChild(i).position, Quaternion.Euler(staffRotation));
                    break;
                case "FireStaff":
                    weapon = Instantiate(FireStaffPrefab, player.transform.GetChild(i).position, Quaternion.Euler(staffRotation));
                    break;
                case "WaterStaff":
                    weapon = Instantiate(WaterStaffPrefab, player.transform.GetChild(i).position, Quaternion.Euler(staffRotation));
                    break;
                case "NormalSword":
                    weapon = Instantiate(NormalSwordPrefab, player.transform.GetChild(i).position, Quaternion.identity);
                    break;
                default:
                    Debug.LogWarning("Unknown weapon name: " + weaponName);
                    break;
            }

            if (weapon != null)
            {
                weapon.transform.position = Vector3.zero;
                weapon.transform.SetParent(player.transform.GetChild(i), false);
                break; 
            }
        }
    }

    public void SetWeapon(string weaponName)
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weapon = null;


        for (int i = 0; i < player.transform.childCount; i++)
        {

            if (player.transform.GetChild(i).childCount > 0)
            {
                continue;
            }


            switch (weaponName)
            {
                case "AK47":
                    weapon = Instantiate(Ak47Prefab,Vector3.zero, Quaternion.identity);
                    break;
                case "Sniper":
                    weapon = Instantiate(SniperPrefab, Vector3.zero, Quaternion.identity);
                    break;
                case "Shotgun":
                    weapon = Instantiate(ShotgunPrefab, Vector3.zero, Quaternion.identity);
                    break;
                case "LaserGun":
                    weapon = Instantiate(LaserGunPrefab, Vector3.zero, Quaternion.identity);
                    break;
                case "Bazooka":
                    weapon = Instantiate(BazookaPrefab, Vector3.zero, Quaternion.identity);
                    break;
                case "ThunderStaff":
                    weapon = Instantiate(ThunderStaffPrefab, Vector3.zero, Quaternion.Euler(staffRotation));
                    break;
                case "DarkStaff":
                    weapon = Instantiate(DarkStaffPrefab, Vector3.zero, Quaternion.Euler(staffRotation));
                    break;
                case "FireStaff":
                    weapon = Instantiate(FireStaffPrefab, Vector3.zero, Quaternion.Euler(staffRotation));
                    break;
                case "WaterStaff":
                    weapon = Instantiate(WaterStaffPrefab, Vector3.zero, Quaternion.Euler(staffRotation));
                    break;
                case "NormalSword":
                    weapon = Instantiate(NormalSwordPrefab, Vector3.zero, Quaternion.identity);
                    break;
                default:
                    Debug.LogWarning("Unknown weapon name: " + weaponName);
                    break;
            }

            if (weapon != null)
            {
                weapon.transform.position = Vector3.zero;
                weapon.transform.SetParent(player.transform.GetChild(i), false);
                
                break;
            }

            Debug.Log("WeaponSLotsAreFull");
        }
    }

    public void UnEquipAWeapon(int Slotindex)
    {
        Debug.Log(Slotindex);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player.transform.GetChild(Slotindex).transform.childCount>0)
        {
            Transform transform = player.transform.GetChild(Slotindex).transform.GetChild(0);
            GameObject weaponEquiped;
            if (transform != null)
            {
                weaponEquiped = transform.gameObject;
                Destroy(weaponEquiped);
            }
            else
            {
                return;
            }
        }
        else
        {
            return ;
        }
           
       
       
       

        
    }


    IEnumerator SpawnMonsterFromList(Monster monster)
    {
        int maxSpawn = monster.number;
        int totalSpawned = 0;

        while (totalSpawned < maxSpawn   )
        {
            yield return new WaitForSeconds(monster.timeDelay);
            
            int n = UnityEngine.Random.Range(1, Mathf.Min(3, maxSpawn - totalSpawned + 1)); 

            for (int i = 0; i < n; i++)
            {
                if (totalSpawned >= maxSpawn || isEndWave)
                    break;


                Vector2 position;
                while (true)
                {
                     x = UnityEngine.Random.Range(-MaxX, MaxX); y = UnityEngine.Random.Range(-MaxY, MaxY);
                     position = new Vector3(x, y);

                    if (SpawmZone.OverlapPoint(position)) break;
                }
               
                   
               
                GameObject spawnmonster = MonsterPoolManager.Instance.GetMonster(monster.monsterType);
                spawnmonster.transform.position = position;
                spawnmonster.transform.rotation = Quaternion.identity;  
                totalSpawned++;
            }

          
        }
    }
    IEnumerator StartWaveTime(int waveTime)
    {
        while (waveTime >= 0)
        {
            WaveTimeText.text = waveTime.ToString() + "s";
            WaveNumberText.text = "Wave" +" "+ (currentWave + 1).ToString();
            yield return new WaitForSeconds(1); // Chờ 1 giây
            waveTime -= 1;
            
            
        }
        
        EndWave(currentWave);
        
    }

    IEnumerator GetAllTreasure()
    {
        yield return new WaitForSeconds(1f);
        GetAllCollectable?.Invoke();
    }

    IEnumerator OpenChestMenuCorroutine()
    {
        yield return new WaitForSeconds(2f);
        CharacterStartMenu.SetActive(false);
        OpenChestMenu.SetActive(true);
    }
}
