using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadEnd"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }
}
