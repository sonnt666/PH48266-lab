using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;           // Tốc độ di chuyển của quái
    public float changeDirectionTime = 2f; // Thời gian đổi hướng
    private Vector2 movement;
    private float timer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Gán SpriteRenderer và đặt thời gian để chọn hướng ngẫu nhiên ban đầu
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = changeDirectionTime;
        ChooseNewDirection();
    }

    void Update()
    {
        // Đếm thời gian để thay đổi hướng
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChooseNewDirection();
            timer = changeDirectionTime;
        }

        // Di chuyển quái
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Kiểm tra và lật mặt quái dựa trên hướng di chuyển
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Hướng mặt sang phải
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;  // Hướng mặt sang trái
        }
    }

    // Hàm chọn hướng di chuyển ngẫu nhiên
    void ChooseNewDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movement = new Vector2(randomX, randomY).normalized;
    }

}