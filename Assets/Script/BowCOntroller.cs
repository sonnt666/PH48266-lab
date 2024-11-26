using UnityEngine;

public class BowCOntroller : MonoBehaviour
{
    private Vector2 movement;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;
    public Transform firePoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void Shoot()
    {
        // Tạo đạn tại vị trí của firePoint và theo hướng nhân vật đang đối diện
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Đặt hướng của đạn dựa vào hướng của nhân vật
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(Vector2.right);
    }
}
