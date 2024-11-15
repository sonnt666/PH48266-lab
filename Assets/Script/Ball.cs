using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 2f;                 // Tốc độ di chuyển của enemy
    public float moveDistance = 3f;          // Khoảng cách di chuyển lên xuống
    public float detectionRange = 5f;        // Phạm vi phát hiện người chơi
    public Transform player;                 // Tham chiếu đến người chơi

    private Vector2 startPosition;
    private bool movingUp = true;            // Hướng di chuyển ban đầu
    private bool isChasingPlayer = false;    // Kiểm tra trạng thái đang theo người chơi

    void Start()
    {
        startPosition = transform.position;  // Lưu vị trí ban đầu của enemy
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Kiểm tra nếu người chơi ở trong phạm vi phát hiện
        if (distanceToPlayer <= detectionRange)
        {
            isChasingPlayer = true;
        }
        else
        {
            isChasingPlayer = false;
        }

        if (isChasingPlayer)
        {
            // Di chuyển về phía người chơi
            MoveTowardsPlayer();
        }
        else
        {
            // Thực hiện di chuyển lên xuống
            Patrol();
        }
    }

    void MoveTowardsPlayer()
    {
        // Tính toán hướng đến người chơi
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Patrol()
    {
        // Kiểm tra khoảng cách để đổi hướng
        if (movingUp && transform.position.y >= startPosition.y + moveDistance)
        {
            movingUp = false;
        }
        else if (!movingUp && transform.position.y <= startPosition.y - moveDistance)
        {
            movingUp = true;
        }

        // Di chuyển enemy lên hoặc xuống
        Vector2 direction = movingUp ? Vector2.up : Vector2.down;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
