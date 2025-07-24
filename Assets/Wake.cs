using UnityEngine;
using UnityEngine.SceneManagement;

public class Wake : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<Player>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Đăng ký callback mỗi khi scene load xong
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Gỡ callback để tránh lỗi khi object bị huỷ
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            Debug.Log("Scene là Menu → Huỷ Player giữ bằng DontDestroy");

            Destroy(gameObject);
        }
    }
}
