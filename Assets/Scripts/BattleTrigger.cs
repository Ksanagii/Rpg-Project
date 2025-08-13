using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    public static bool startBattle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startBattle = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(EnemyScene.detect)
            {
                // INICIAR BATALHA NORMAL
                Debug.Log("Normal battle Started");
                startBattle = true;
                BattleManager.Instance.lastEnemyBattled = gameObject; // Salva o inimigo atual no BattleManager
                StartCoroutine(WaitForSecondsToBattle(2f)); // Espera 2 segundos antes de iniciar a batalha
            }
            else
            {
                // INICIAR BATALHA COM ALGUM BUFF PRO PLAYER (A DECIDIR)
                Debug.Log("Buffed battle Started");
                startBattle = true;
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
