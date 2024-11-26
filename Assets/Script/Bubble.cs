using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Sprite[] sprites;
    public float gravityChangeInterval = 2f; // Khoảng thời gian giữa các lần thay đổi
    public float minGravity = -0.5f; // Giá trị nhỏ nhất của gravityScale
    public float maxGravity = -1f; // Giá trị lớn nhất của gravityScale
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && sprites.Length > 0)
        {
            // Random chọn sprite
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];
        }

        StartCoroutine(RandomizeGravity());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadEnd")||collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator RandomizeGravity()
    {
        while (true)
        {
            // Thay đổi gravityScale ngẫu nhiên
            rb.gravityScale = Random.Range(minGravity, maxGravity);

            // Chờ một khoảng thời gian trước khi thay đổi lại
            yield return new WaitForSeconds(gravityChangeInterval);
        }
    }
}
