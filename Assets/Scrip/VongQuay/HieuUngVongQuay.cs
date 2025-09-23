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
    public float timetiaset = 10f;
    public float timetiaset1 = 10f;
    public float timetiaset2 = 10f;

    [Header("Time sat thuong")]
    public GameObject satthuong;
    public float timest = 10f;
    public float currentTimess;
    public TextMeshProUGUI sttext;

    [Header("Cỏ may mắn ( thêm 1 mạng / cỏ ")]
    public GameObject Comayman;
    public TextMeshProUGUI textcomayman;
    public int mangthem = 0;
    



    public static HieuUngVongQuay instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        currentTimespeedbuff = timespeed;
        currentTimehpbuff = timehp;
        currentTimess = timest;
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
    
    //năng lượng vàng ( tăng coin nhận vào gấp 5 lần trong 5s )
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


    //năng lượng đen ( tăng chống chịu 5s)
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

    //năng lượng đỏ ( bất tử 5s )
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


    /// <summary>
    /// buff sat thuong vong quay
    /// </summary>
    public void satthuongbuff()
    {
        StartCoroutine(STBuffCountdown());
    }
    public IEnumerator STBuffCountdown()
    {
        satthuong.SetActive(true);
        currentTimess = timest;
        while (currentTimess > 0)
        {
            currentTimess -= Time.deltaTime;
            sttext.text = currentTimess.ToString("F1") + "s";
            yield return null; // chờ frame tiếp theo
        }
        satthuong.SetActive(false);
    }

    /// <summary>
    /// Cỏ may mắn them mạn
    /// </summary>
    public void Comaymanbuff()
    {
      Comayman.SetActive(true);
        mangthem += 1;
      textcomayman.text = $"{mangthem} mạng";
    }
    public void UpdateTextCo()
    {
       textcomayman.text = $"{mangthem} mạng";
        if(mangthem <= 0)
        {
            Comayman.SetActive(false);
        }
    }

    private void OnEnable()
    {
        PhanThuong.OnThuong += SpeedBuff;
        PhanThuong.OnThuongHp += HpBuff;
        PhanThuong.OnThuongTiaset += TiaSetBuff;
        PhanThuong.OnThuongTiaset1 += TiaSetBuff1;
        PhanThuong.OnThuongTiaset2 += TiaSetBuff2;
        PhanThuong.sathuongbuff += satthuongbuff;
        PhanThuong.comaymanbuff += Comaymanbuff;
    }
    private void OnDisable()
    {
        PhanThuong.OnThuong -= SpeedBuff;
        PhanThuong.OnThuongHp -= HpBuff;
        PhanThuong.OnThuongTiaset -= TiaSetBuff;
        PhanThuong.OnThuongTiaset1 -= TiaSetBuff1;
        PhanThuong.OnThuongTiaset2 -= TiaSetBuff2;
        PhanThuong.sathuongbuff -= satthuongbuff;
        PhanThuong.comaymanbuff -= Comaymanbuff;

    }
}
