using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    public float bulletSpeed = 10f;
    private Vector2 movement;
    private Animator anim;
    public GameObject laser;
    public Transform firePoint;
    public GameObject Explove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
    private void Shoot()
    {
        // Tạo đạn tại vị trí của firePoint và theo hướng nhân vật đang đối diện
        GameObject bullet = Instantiate(laser, firePoint.position, firePoint.rotation);

        // Kiểm tra xem đạn có Rigidbody2D hay không
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            // Đặt vận tốc cho đạn
            bulletRb.linearVelocity = firePoint.up * bulletSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        {
            Destroy(gameObject);

            Instantiate(Explove, transform.position, Quaternion.identity);
        }
    }
}
