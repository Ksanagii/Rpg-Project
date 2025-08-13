using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public GameObject player;
    public Vector3 playerTransform; // Player posicao
    public GameObject lastEnemyBattled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; // chama o metodo OnSceneLoaded quando uma cena é carregada

    }

    void Start()
    {

    }

    void Update()
    {
        if(BattleTrigger.startBattle && player != null)
        {
            playerTransform = player.transform.position;
        }

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Mundo" && BattleManager.Instance.lastEnemyBattled != null)
        {
            Destroy(BattleManager.Instance.lastEnemyBattled);
            BattleManager.Instance.lastEnemyBattled = null; // Limpa a referência
        }
    }
}
