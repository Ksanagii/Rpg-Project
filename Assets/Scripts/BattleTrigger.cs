using System.Collections;
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
            if (BattleManager.Instance.player != null)
            {
                BattleManager.Instance.SetPlayerPosition(); // Salva a posicao do player no BattleManager
                Debug.Log("posicao do player salva");
            }
            

            // Verifica se o inimigo esta detectando o player
            // Se sim, inicia uma batalha normal, se nao, inicia uma batalha com algum buff pro player
            // (A logica de buff ainda nao foi implementada, entao por enquanto so inicia uma batalha normal)

            if (GetComponentInParent<EnemyScene>().detect)
            {
                // INICIAR BATALHA NORMAL
                Debug.Log("Normal battle Started");
                startBattle = true;
                BattleManager.Instance.battleEnemiesID.Add(GetComponentInParent<EnemyScene>().enemyID);
                BattleManager.Instance.enemySO = GetComponentInParent<EnemyScene>().enemyData;
                StartCoroutine(WaitForSecondsToBattle(0.5f)); // Espera 1 segundo antes de iniciar a batalha (para alguma animação ou efeito visual)
            }

            
            else
            {
                // INICIAR BATALHA COM BUFF
                Debug.Log("Buffed battle Started");
                BattleManager.Instance.battleEnemiesID.Add(GetComponentInParent<EnemyScene>().enemyID);
                BattleManager.Instance.enemySO = GetComponentInParent<EnemyScene>().enemyData;
                startBattle = true;
                StartCoroutine(WaitForSecondsToBattle(0.5f));

            }
            

        }
    }

    IEnumerator WaitForSecondsToBattle(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("BattleScene");
    }
}
