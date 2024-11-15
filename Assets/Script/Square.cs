using UnityEngine;
using UnityEngine.SceneManagement;

public class Square : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 movement;
    public float speed;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem có va chạm với enemy không
        if (other.CompareTag("Enemy"))
        {
            // Dịch chuyển người chơi về vị trí xuất phát
            transform.position = startPosition;
        }
        if (other.CompareTag("Win"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
