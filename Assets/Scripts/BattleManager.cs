using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public GameObject player;
    public Vector3 playerTransform; // Player posicao
    public List<string> battleEnemiesID = new List<String>(); // ID do inimigo da batalha atual
    public EnemyScriptableObject enemySO; // referencia do inimigo da batalha atual

    private void Awake()
    {
        Debug.Log("teste awake");
        if (Instance != null && Instance != this) // Verifica se ja existe uma instancia do BattleManager
        {
            Destroy(this.gameObject); // Se ja existir, destrua este objeto
        }
        else
        {
            Instance = this; // Se nao existir, defina esta instancia como a atual
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // chama o metodo OnSceneLoaded quando uma cena e carregada
        }



    }

    void Start()
    {
        Debug.Log("teste Start");
    }

    void Update()
    {

        #region MUDAR CENAS COM 1 BOTAO APENAS POR TESTE (M P/ MUNDO, B P/ BATALHA)
        if (Input.GetKey(KeyCode.M))
        {
            LoadScene("Mundo");
        }
        if (Input.GetKey(KeyCode.B))
        {
            LoadScene("BattleScene");
        }
        #endregion
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded");
        if (scene.name == "Mundo")
        {
            if (battleEnemiesID.Count > 0) // Verifica se a lista de IDs de inimigos da batalha atual nao esta vazia
            {
                Debug.Log("Removendo " + battleEnemiesID.Count + " inimigo(s) da cena");
                EnemyScene[] enemies = FindObjectsByType<EnemyScene>(FindObjectsSortMode.None); // Encontra todos os inimigos na cena
                Debug.Log("Inimigos encontrados: " + enemies.Length);
                foreach (EnemyScene enemy in enemies) // para cada inimigo da cena
                {
                    foreach (string enemyID in battleEnemiesID) // para cada ID de inimigo na lista de IDs de inimigos enfrentados
                    {
                        if (enemy.enemyID == enemyID) // se o ID do inimigo da cena for igual ao ID do inimigo na lista de IDs de inimigos enfrentados
                        {
                            Destroy(enemy.gameObject); // destrua o inimigo da cena
                            Debug.Log("Inimigo removido: " + enemy.enemyID);
                        }
                    }
                }
            }

            if (GameObject.FindGameObjectWithTag("Player") != null) // referencia do player
                player = GameObject.FindGameObjectWithTag("Player");
            else { Debug.LogWarning("Player nao encontrado"); }

            if (player != null && playerTransform != Vector3.zero)
            {
                player.transform.position = playerTransform; // Define a posicao do player para a posicao salva no BattleManager
            }
        }
        // battleEnemyID = null; // Limpa a referencia

    }

    public void SetPlayerPosition()
    {
        playerTransform = player.transform.position;
    }
}
