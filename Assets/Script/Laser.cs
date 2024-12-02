using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;          // Tốc độ của đạn
    private Vector2 direction;         // Hướng di chuyển của đạn

    void Start()
    {

    }

    void Update()
    {
        // Di chuyển đạn theo hướng với tốc độ không đổi
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đạn va chạm với đối tượng có tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);  // Tiêu diệt Enemy
            Destroy(gameObject);        // Hủy đạn sau khi va chạm
        }
        else if (other.CompareTag("DeadEnd"))
        {
            Destroy(gameObject);
        }
    }
}
