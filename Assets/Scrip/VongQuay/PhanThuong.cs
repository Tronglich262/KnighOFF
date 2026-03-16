using UnityEngine;
public enum PhanThuongType
{
    o1,
    o2,
    o3,
    o4,
    o5,
    o6,
    o7,
    o8,
    o9,
    o10,
}
public class PhanThuong : MonoBehaviour
{
    public delegate void thuong();
    public static event thuong OnThuong;
    public static event thuong OnThuongHp;
    public static event thuong OnThuongTiaset;
    public static event thuong OnThuongTiaset1;
    public static event thuong OnThuongTiaset2;
    public static event thuong sathuongbuff;
    public static event thuong comaymanbuff;
    public PhanThuongType phanThuongType;
   
    /// <summary>
    /// phần thường
    /// </summary>
    public void NhanThuong()
    {
        switch (phanThuongType)
        {
            case PhanThuongType.o9:
                OnThuongHp?.Invoke();
                break;
            case PhanThuongType.o10:
                ScoreManager.Instance.AddScore(500);
                break;
            case PhanThuongType.o8:
                OnThuong?.Invoke();
                break;
            case PhanThuongType.o7:
                OnThuongTiaset?.Invoke();
                break;
            case PhanThuongType.o6:
                OnThuongTiaset1?.Invoke();
                break;
            case PhanThuongType.o5:
                OnThuongTiaset2?.Invoke();
                break;
            case PhanThuongType.o4:
                comaymanbuff?.Invoke();
                break;
            case PhanThuongType.o3:
                //quang cao
                break;
            case PhanThuongType.o2:
                sathuongbuff?.Invoke();
                break;
            case PhanThuongType.o1:
                // chúc may mắn lần sau
                break;
        }
    }

}
