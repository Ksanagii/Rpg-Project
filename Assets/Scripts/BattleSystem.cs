using TMPro;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattlePoint;
    public Transform enemyBattlePoint;

    public static BattleState state;

    public Unit playerUnit;
    public Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void Update()
    {
        
    }

    void SetupBattle()
    {
        GameObject PlayerGo = Instantiate(playerPrefab, playerBattlePoint);
        playerUnit = PlayerGo.GetComponent<Unit>();

        GameObject EnemyGo = Instantiate(enemyPrefab, enemyBattlePoint);
        enemyUnit = EnemyGo.GetComponent<Unit>();

        dialogueText.text = "O " + enemyUnit.unitID + " Se aproximou!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
    }
}
