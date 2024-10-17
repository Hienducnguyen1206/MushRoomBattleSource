using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public TextMeshProUGUI WaveTimeText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWave(25));
    }

    // Update is called once per frame
    void Update()
    {

    }







    IEnumerator StartWave(int waveTime)
    {
        while (waveTime > 0)
        {
            yield return new WaitForSeconds(1); // Chờ 1 giây
            waveTime -= 1;
            WaveTimeText.text = waveTime.ToString() + "s";
            Debug.Log("Wave time left: " + waveTime);
        }

        Debug.Log("endWave");
    }





}
