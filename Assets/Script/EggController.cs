using System.Collections;
using UnityEngine;

public class EggController : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject brokenEggPrefab; // Prefab trứng vỡ
    public int lifePenalty = 1;        // Mạng bị trừ khi trứng vỡ

    private GameManager gameManager;   // Tham chiếu đến GameManager

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Tìm GameManager trong scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Tăng điểm
            gameManager?.AddScore();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("DeadEnd"))
        {
            // Trừ mạng và xử lý trứng vỡ
            gameManager?.LoseLife();
            StartCoroutine(HandleBrokenEgg());
        }
    }

    private IEnumerator HandleBrokenEgg()
    {
        if (brokenEggPrefab != null)
        {
            GameObject brokenEgg = Instantiate(brokenEggPrefab, transform.position, Quaternion.identity);

            // Tăng kích thước trứng vỡ
            Transform brokenEggTransform = brokenEgg.transform;
            Vector3 initialScale = brokenEggTransform.localScale;
            Vector3 targetScale = initialScale * 1.5f;
            float growDuration = 0.5f;
            float elapsedTime = 0f;

            while (elapsedTime < growDuration)
            {
                elapsedTime += Time.deltaTime;
                brokenEggTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / growDuration);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            Destroy(brokenEgg);
        }

        Destroy(gameObject);
    }
}
