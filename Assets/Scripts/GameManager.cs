using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Text timer;
    public int timerCounter = 120;
    bool isTimerOn = false;
    public CinemachineVirtualCamera cmFreeCam;
    public GameObject explosion;
    public GameObject whiteScreen;
    public AudioSource bgMusic;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        StartCoroutine(_ProcessShake());
    }
    public void StartTimer()
    {
 
        if (!isTimerOn)
        {
            StartCoroutine(Timeeee());
            isTimerOn = true;
        }
    }


    IEnumerator Timeeee()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timerCounter--;
            string s = "0" + (Mathf.FloorToInt(timerCounter / 60)).ToString() + ":" + (timerCounter % 60).ToString();
            timer.text = s;

            if (timerCounter <= 0)
            {
                StartExplosion();
            }
        }
    }

    public void StartExplosion()
    {
        bgMusic.enabled = false;
        StartCoroutine(Exp());
    }

    IEnumerator Exp()
    {
        explosion.gameObject.SetActive(true);
        //trigger sound
        yield return new WaitForSeconds(2);
        whiteScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator _ProcessShake(float shakeIntensity =3f, float shakeTiming = 0.5f)
    {
        while (true)
        {
            float rand = Random.Range(1f, 3f);
            yield return new WaitForSeconds(rand);
            Noise(1, shakeIntensity);
            yield return new WaitForSeconds(shakeTiming);
            Noise(0, 0);
        }
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;

    }

}
