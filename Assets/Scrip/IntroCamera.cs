using System.Collections;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    [Header("Cài đặt đường đi camera")]
    public Transform[] pathPoints;
    public float moveSpeed = 3f;

    [Header("Người chơi")]
    public GameObject player;
    private bool followPlayer = false;

    [Header("Thông báo")]
    public GameObject imagethongbao;
    public GameObject ThamquanMap;

    private Vector3 offset;  // Will be computed dynamically

    private void Start()
    {
        StartCoroutine(WaitForPlayerThenIntro());
    }

    IEnumerator deleythongbao()
    {
        yield return new WaitForSeconds(0.5f);
        imagethongbao.SetActive(true);
        yield return new WaitForSeconds(2f);
        imagethongbao.SetActive(false);

        player.GetComponent<Player>().enabled = true;
        player.GetComponent<Player1>().enabled = true;
        ActiveSkillAndMenu.instance.unactivebtnall();
    }
    

    IEnumerator WaitForPlayerThenIntro()
    {
        while (player == null)
        {
            player = GameObject.FindWithTag("Player");
            yield return null;
        }
        if (player != null)
        {
            // Compute offset from initial camera/player positions
            offset = transform.position - player.transform.position;
            player.GetComponent<Player>().enabled = false;
            player.GetComponent<Player1>().enabled = false;
            ActiveSkillAndMenu.instance.activebtnall();
            yield return new WaitForSeconds(0.5f);
            ThamquanMap.SetActive(true);
            yield return new WaitForSeconds(2f);
            ThamquanMap.SetActive(false);
        }

        yield return StartCoroutine(PlayIntro());

        // Sau intro thì follow Player
        followPlayer = true;
    }

    IEnumerator PlayIntro()
    {
        // Camera bay qua các điểm
        for (int i = 0; i < pathPoints.Length; i++)
        {
            Vector3 target = new Vector3(
                pathPoints[i].position.x,
                pathPoints[i].position.y,
                transform.position.z
            );

            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        // Quay trở lại vị trí camera follow player
        float duration = 2f;
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = player.transform.position + offset;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        StartCoroutine(deleythongbao());
    }

    private void LateUpdate()
    {
        if (followPlayer && player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }
}