using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector2 direction;         // Hướng di chuyển của đạn

    void Start()
    {

    }

    void Update()
    {

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
        if (other.CompareTag("Meteor"))
        {
            Destroy(gameObject);
        }
    }
}
