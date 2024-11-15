using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;          // Tốc độ di chuyển của nhân vật
    private Rigidbody2D rb;               // Rigidbody2D để điều khiển vật lý nhân vật
    private Animator animator;            // Animator để điều khiển animation
    private bool facingRight = true;      // Kiểm tra hướng của nhân vật
    public GameObject AtkZone;
    public GameObject bulletPrefab;           // Prefab của đạn (Bullet)
    public Transform firePoint;               // Vị trí bắn đạn
    private Vector2 movement;             // Vector để giữ hướng di chuyển

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Lấy input từ bàn phím
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Cập nhật biến "isRunning" trong Animator dựa vào trạng thái di chuyển
        bool isRunning = movement.x != 0 || movement.y != 0;
        animator.SetBool("IsRunning", isRunning);

        // Kiểm tra hướng di chuyển để flip nhân vật
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        // Đảo hướng của nhân vật
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void Attack()
    {
        animator.SetTrigger("IsAtk");
        // Tạo một vùng tấn công `AtkZone` tại vị trí của nhân vật
        GameObject atkzone = Instantiate(AtkZone, transform.position, Quaternion.identity, transform);
    }
    private void Shoot()
    {
        // Tạo đạn tại vị trí của firePoint và theo hướng nhân vật đang đối diện
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Đặt hướng của đạn dựa vào hướng của nhân vật
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(facingRight ? Vector2.right : Vector2.left);
    }
}
