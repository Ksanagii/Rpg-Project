using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float velocity;
    Rigidbody2D rb;
    public static Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        CameraFollow cameraFollow = FindFirstObjectByType<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.ResetCameraPosition(transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {



    }

    void FixedUpdate()
    {
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        direction = new(xRaw, yRaw);
        if (direction.magnitude > 1)
            direction = direction.normalized; // Normaliza a direcao para que a velocidade seja constante nas diagonais

        // Move o player
        transform.position = new(
            transform.position.x + (direction.x * velocity * Time.fixedDeltaTime),
            transform.position.y + (direction.y * velocity * Time.fixedDeltaTime));

    }
}
