using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float velocity;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (BattleManager.Instance != null && BattleManager.Instance.playerTransform != Vector3.zero && SceneManager.GetActiveScene().name == "Mundo") // Verifica se o BattleManager existe, se a posicao do player nao eh zero e se a cena atual eh "Mundo"
        {
            transform.position = BattleManager.Instance.playerTransform; // Define a posicao do player para a posicao salva no BattleManager
        }
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
