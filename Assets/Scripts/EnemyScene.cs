using UnityEngine;

public class EnemyScene : MonoBehaviour
{
    public bool detect;
    public string enemyID;
    [SerializeField] float moveSpeed;
    public GameObject player;
    public EnemyScriptableObject enemyData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        detect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(detect)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 
                moveSpeed * Time.deltaTime); // move em direcao ao player
            
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
}
