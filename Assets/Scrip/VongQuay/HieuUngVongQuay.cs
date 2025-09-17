using System.Collections;
using TMPro;
using UnityEngine;

public class HieuUngVongQuay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Tốc độ chạy")]
    public float timespeed = 10f;
    public float currentTimespeedbuff;
    public GameObject speed;
    public TextMeshProUGUI speedtext;


    [Header("Máu")]
    public float timehp = 5f;
    public float currentTimehpbuff;
    public GameObject hp;
    public TextMeshProUGUI hptext;

    [Header("Tia set")]
    public GameObject tiaSet;
    public GameObject tiaSet1;
    public GameObject tiaSet2;


    [Header("Time buff tia set")]
    public float timetiaset = 5f;
    public float timetiaset1 = 10f;
    public float timetiaset2 = 15f;


    void Start()
    {
        currentTimespeedbuff = timespeed;
        currentTimehpbuff = timehp;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    /// <summary>
    /// buff hp
    /// </summary>
    public void HpBuff()
    {
        StartCoroutine(HpBuffCountdown());
    }
    public IEnumerator HpBuffCountdown()
    {
        hp.SetActive(true);
        currentTimehpbuff = timehp;
        while (currentTimehpbuff > 0)
        {
            currentTimehpbuff -= Time.deltaTime;
            hptext.text = currentTimehpbuff.ToString("F1") + "s";
            yield return null; // chờ frame tiếp theo
        }
        hp.SetActive(false);
    }



    /// <summary>
    /// buff tốc độ
    /// </summary>
    public void SpeedBuff()
    {
       StartCoroutine(BuffCountdown());
    }
    private IEnumerator BuffCountdown()
    {
        speed.SetActive(true);
        currentTimespeedbuff = timespeed;

        while (currentTimespeedbuff > 0)
        {
            currentTimespeedbuff -= Time.deltaTime;
            speedtext.text = currentTimespeedbuff.ToString("F1") + "s";
            yield return null; // chờ frame tiếp theo
        }

        speed.SetActive(false);
    }

    /// <summary>
    /// tia set
    /// </summary>
    public void TiaSetBuff()
    {
        StartCoroutine(deleyTime());
    }
    public IEnumerator deleyTime()
    {
        tiaSet.SetActive(true);

        float timer = timetiaset; 
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null; 
        }

        tiaSet.SetActive(false);
    }
    public void TiaSetBuff1()
    {
        StartCoroutine(deleyTime1());
    }
    public IEnumerator deleyTime1()
    {
        tiaSet1.SetActive(true);

        float timer = timetiaset1; 
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null; 
        }

        tiaSet1.SetActive(false);
    }
    public void TiaSetBuff2()
    {
        StartCoroutine(deleyTime2());
    }
    public IEnumerator deleyTime2()
    {
        tiaSet2.SetActive(true);

        float timer = timetiaset2; 
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null; 
        }

        tiaSet2.SetActive(false);
    }


    private void OnEnable()
    {
        PhanThuong.OnThuong += SpeedBuff;
        PhanThuong.OnThuongHp += HpBuff;
        PhanThuong.OnThuongTiaset += TiaSetBuff;
        PhanThuong.OnThuongTiaset1 += TiaSetBuff1;
        PhanThuong.OnThuongTiaset2 += TiaSetBuff2;
    }
    private void OnDisable()
    {
        PhanThuong.OnThuong -= SpeedBuff;
        PhanThuong.OnThuongHp -= HpBuff;
        PhanThuong.OnThuongTiaset -= TiaSetBuff;
        PhanThuong.OnThuongTiaset1 -= TiaSetBuff1;
        PhanThuong.OnThuongTiaset2 -= TiaSetBuff2;

    }
}
