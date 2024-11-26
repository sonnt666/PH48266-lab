using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private bool isRandomAttacking = false;
    public Image healthBar;
    public float healAmount = 100f;
    private bool canTakeDamage = true; // Kiểm soát thời gian giữa các lần bị tấn công
    public float damageCooldown = 1.5f;

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
            StartCoroutine(AttackAnimation());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(BurstAttack());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isRandomAttacking)
            {
                StartCoroutine(RandomAttack());
            }
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
    private IEnumerator BurstAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            // Tạo đạn tại vị trí của firePoint và theo hướng nhân vật đang đối diện
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Đặt hướng của đạn dựa vào hướng của nhân vật
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(facingRight ? Vector2.right : Vector2.left);


            // Chờ 1 giây trước khi bắn viên tiếp theo
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator RandomAttack()
    {
        int bulletCount = 3; // Số viên đạn cần bắn

        for (int i = 0; i < bulletCount; i++)
        {
            // Tạo đạn tại vị trí của firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Đặt hướng ngẫu nhiên cho viên đạn
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(randomDirection);

            Destroy(bullet, 0.2f);

            // Chờ 0.1 giây trước khi bắn viên tiếp theo
            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator AttackAnimation()
    {
        animator.SetTrigger("IsAtk");
        yield return new WaitForSeconds(0.5f); // Thời gian của animation
        animator.SetTrigger("IsIdle");
    }
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            healAmount -= 20;
            healthBar.fillAmount = healAmount/100f;

            StartCoroutine(DamageCooldown());
        }
    }

    private System.Collections.IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu dùng trigger thay vì collision
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(); // Lượng sát thương cố định, bạn có thể tùy chỉnh
        }
    }
}
