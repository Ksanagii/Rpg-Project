using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float velocity;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    // Update is called once per frame
    void Update()
    {
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(xRaw, yRaw);

        transform.position = new Vector2(transform.position.x + (direction.x * velocity * Time.deltaTime), transform.position.y + (direction.y * velocity * Time.deltaTime));
    }
}
