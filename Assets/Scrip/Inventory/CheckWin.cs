using System.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CheckWin : MonoBehaviour
{
    public GameObject PanelWin;
    public Image yeusao;
    public Image trungbinhsao;
    public Image totsao;
    public static CheckWin Instance;
    public Image huyhieuyeu;
    public Image huyhieutrungbinh;
    public Image huyhieutot;
    public AudioClip AudioClip;
    public AudioClip sao;
    private AudioSource audioSource;
    public void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PanelWin != null)
                    {
            PanelWin.SetActive(false);
        }
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        var check = GetComponent<DialogueTrigger>();
        if (check != null && check.checkwin == 2)
        {
            if (audioSource != null && AudioClip != null)
            {
                audioSource.PlayOneShot(AudioClip);
            }

            PanelWin.SetActive(true);
            check.checkwin = 0;
            Debug.Log("da chay");
            StartCoroutine(lanluothiensao());
        }
    }
    IEnumerator lanluothiensao()
    {
        yield return new WaitForSeconds(0.5f);
        if (ScoreManager.Instance.currentScore >= 1000)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieutot.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);
            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            trungbinhsao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            totsao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

        }
        else if (ScoreManager.Instance.currentScore >= 600)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieutrungbinh.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            trungbinhsao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

        }
        else if (ScoreManager.Instance.currentScore >= 300)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieuyeu.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);

        }

    }

}
