using UnityEngine;

public class DetectZone : MonoBehaviour
{
    EnemyScene enemyScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyScene = GetComponentInParent<EnemyScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScene.detect)
        {
            RotateTowardsPlayer();
        }
        else
        {
            RotateWithMovement();
        }

    }

    void RotateTowardsPlayer()
    {
        Vector2 offset = transform.position - enemyScene.player.transform.position;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Ajusta a rotacao do objeto para apontar na direcaoo do player
    }
    void RotateWithMovement()
    {
        // Pega a direção atual do movimento do inimigo
        Vector2 moveDirection = enemyScene.GetMoveDirection();
        
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(-moveDirection.y, -moveDirection.x) * Mathf.Rad2Deg; // transforma o vetor em angulo e depois em graus
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyScene.detect = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyScene.detect = false;
        }
    }
    
}
