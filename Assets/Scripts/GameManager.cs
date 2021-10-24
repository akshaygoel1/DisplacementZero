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

    public GameObject logo;
    public List<GameObject> hud = new List<GameObject>();
    public GameObject blocker;
    public CanvasGroup creditsScreen;
    public AudioSource normal, end;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        StartCoroutine(_ProcessShake());


        if (PlayerPrefs.GetInt("crossedFirstLoop", 0) == 1)
        {
            CharacterController.instance.TriggerPlayerDialog("Wait? Was there an explosion? Why am I here again?");
            PlayerPrefs.SetInt("crossedFirstLoop", 2);
        }

        if (PlayerPrefs.GetInt("crossedFirstLoop", 0) == 0)
        {
            PlayerPrefs.SetInt("crossedFirstLoop", 1);
        }

        if (PlayerPrefs.GetInt("diffuse", 0) == 1)
        {
            CharacterController.instance.TriggerPlayerDialog("Waaait? I thought it was over!");
            //change bg music
            normal.enabled = false;
            end.enabled = true;
            blocker.SetActive(true);
        }

    }
    public void StartTimer()
    {

        if (!isTimerOn)
        {
            logo.SetActive(false);
            foreach (GameObject g in hud)
                g.SetActive(true);
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


            if (timerCounter == 8)
            {
                SoundManager.instance.PlaySound("explosion");
            }

            if (timerCounter <= 0 && PlayerPrefs.GetInt("diffuse", 0) == 0)
            {
                StartExplosion();
            }
            else if (timerCounter <= 0 && PlayerPrefs.GetInt("diffuse", 0) == 1)
            {
                bgMusic.enabled = false;
                StartCoroutine(Exp2());
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

    IEnumerator Exp2()
    {
        whiteScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator _ProcessShake(float shakeIntensity = 3f, float shakeTiming = 0.5f)
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

    public void End()
    {
        StartCoroutine(Credit());
    }

    IEnumerator Credit()
    {
        yield return new WaitForSeconds(3);
        while (creditsScreen.alpha <= 1)
        {

            yield return new WaitForSeconds(0.01f);
            creditsScreen.alpha += 0.1f;
            creditsScreen.blocksRaycasts = true;
            creditsScreen.interactable = true;
        }
    }
}
