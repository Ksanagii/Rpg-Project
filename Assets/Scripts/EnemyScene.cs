using UnityEngine;

public class EnemyScene : MonoBehaviour
{
    bool detect;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(detect)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime); // move em direcao ao player
        }
    }

    private void OnDrawGizmos()
    {


    }
}
