using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        animator.SetFloat("HorizontalDir", Player.direction.x);
        animator.SetFloat("VerticalDir", Player.direction.y);

        bool isMoving = !Player.direction.Equals(Vector2.zero); // se a direcao for diferente de zero, o player esta se movendo
        animator.SetBool("IsMoving", isMoving);
    }



}
