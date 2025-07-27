using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Xóa nếu đã có 1 instance tồn tại
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Giữ lại khi load scene mới
    }
}
