using UnityEngine;

public class EnemyScene : MonoBehaviour
{
    public bool detect;
    public string enemyID;
    [SerializeField] float moveSpeed;
    public GameObject player;
    public EnemyScriptableObject enemyData;
    Animator anim;
    [SerializeField] float idleTime = 2f; // tempo parado
    [SerializeField] float moveTime = 1f; // tempo de movimento
    private float timer;
    private Vector2 moveDirection;
    private bool isMoving;
    private float lastMoveY;
    private float lastMoveX;
    private SpriteRenderer sprRenderer;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        detect = false;
        anim = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ChooseNewDirection();
        timer = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (detect)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                moveSpeed * Time.deltaTime); // move em direcao ao player

            // Atualiza animação baseado na direção ao player
            Vector2 direction = (player.transform.position - transform.position).normalized;
            anim.SetFloat("moveX", direction.x);
            anim.SetFloat("moveY", direction.y);
            anim.SetBool("isMoving", true);

        }
        else
        {
            // Movimento aleatorio do inimigo quando nao esta detectando o player
            // (A logica de movimento ainda nao foi implementada, entao por enquanto o inimigo fica parado)
            UpdateMovement();

        }
    }

    private void OnDrawGizmos()
    {
        PolygonCollider2D poly = GetComponentInChildren<PolygonCollider2D>();
        if (poly == null) return;

        Gizmos.color = Color.yellow;

        // O PolygonCollider2D pode ter multiplos caminhos (paths)
        for (int p = 0; p < poly.pathCount; p++) // Para cada caminho do poligono
        {
            Vector2[] points = poly.GetPath(p); // Pega os pontos do caminho atual
            for (int i = 0; i < points.Length; i++) // Para cada ponto do caminho
            {
                Vector2 globalA = poly.transform.TransformPoint(points[i]); // Transforma o ponto local para global
                Vector2 globalB = poly.transform.TransformPoint(points[(i + 1) % points.Length]); // Transforma o proximo ponto local para global (usando modulo para fechar o poligono)
                Gizmos.DrawLine(globalA, globalB); // Desenha uma linha entre os pontos
            }
        }

    }

    void ChooseNewDirection()
    {
        /*
        // Escolhe uma das 8 direções
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector2(x, y).normalized;
        
        // Atualiza as variáveis de última direção
        lastMoveX = moveDirection.x;
        lastMoveY = moveDirection.y;
        
        // Atualiza o animator
        anim.SetFloat("moveX", Mathf.Abs(moveDirection.x)); // Sempre usa o valor positivo
        anim.SetFloat("moveY", moveDirection.y);
        sprRenderer.flipX = moveDirection.x < 0; // Flip se for para a esquerda
        isMoving = true;
        anim.SetBool("isMoving", true);
        */

        Vector2[] possibleDirections = new Vector2[]
        {
            Vector2.up,             // Cima (0, 1)
            Vector2.down,           // Baixo (0, -1)
            Vector2.right,          // Direita (1, 0)
            Vector2.left,           // Esquerda (-1, 0)
            new Vector2(1, 1).normalized,    // Diagonal Direita-Cima
            new Vector2(-1, 1).normalized,   // Diagonal Esquerda-Cima
            new Vector2(1, -1).normalized,   // Diagonal Direita-Baixo
            new Vector2(-1, -1).normalized   // Diagonal Esquerda-Baixo
        };

        // Escolhe uma direção aleatória do array
        moveDirection = possibleDirections[Random.Range(0, possibleDirections.Length)];

        // Atualiza as variáveis de última direção
        lastMoveX = moveDirection.x;
        lastMoveY = moveDirection.y;

        // Atualiza o animator
        anim.SetFloat("moveX", Mathf.Abs(moveDirection.x));
        anim.SetFloat("moveY", moveDirection.y);
        sprRenderer.flipX = moveDirection.x < 0;
        isMoving = true;
        anim.SetBool("isMoving", true);
    }

    void UpdateMovement()
    {
        if (!detect)
        {
            timer -= Time.deltaTime;

            if (isMoving)
            {
                // Move o inimigo
                rb.linearVelocity = moveDirection * moveSpeed;

                if (timer <= 0)
                {
                    // Parar de mover
                    isMoving = false;
                    rb.linearVelocity = Vector2.zero;
                    anim.SetBool("isMoving", false);
                    timer = idleTime;

                    // Define o idle baseado na última direção
                    SetIdleAnimation();
                }
            }
            else if (timer <= 0)
            {
                // Escolhe nova direção
                ChooseNewDirection();
                timer = moveTime;
            }
        }
    }

    void SetIdleAnimation()
    {
        // Para diagonais, prioriza o eixo Y
        if (Mathf.Abs(lastMoveY) > 0.1f)
        {
            anim.SetFloat("moveY", lastMoveY);
            anim.SetFloat("moveX", 0);
        }
        else
        {
            anim.SetFloat("moveX", Mathf.Abs(lastMoveX));  // Sempre usa o valor positivo
            anim.SetFloat("moveY", 0);
            // Flip o sprite se estiver indo para a esquerda
            sprRenderer.flipX = lastMoveX < 0;
        }
    }
    
    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}
