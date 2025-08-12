using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(EnemyScene.detect)
            {
                // INICIAR BATALHA NORMAL
                Debug.Log("Normal battle Started");
                StartCoroutine(WaitForSecondsToBattle(2f)); // Espera 2 segundos antes de iniciar a batalha
            }
            else
            {
                // INICIAR BATALHA COM ALGUM BUFF PRO PLAYER (A DECIDIR)
                Debug.Log("Buffed battle Started");
                StartCoroutine(WaitForSecondsToBattle(2f)); // Espera 2 segundos antes de iniciar a batalha

            }


        }
    }

    IEnumerator WaitForSecondsToBattle(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("BattleScene"); // Substitua "BattleScene" pelo nome da sua cena de batalha
    }
}
