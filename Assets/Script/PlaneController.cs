using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    private Vector2 movement;
    private Animator anim;
    public GameObject laser;
    public Transform firePoint;
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

        // Đặt hướng của đạn dựa vào hướng của nhân vật
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(Vector2.up);
    }
}
