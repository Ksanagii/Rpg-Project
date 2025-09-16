using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject actionsCanvas;

    public Transform playerBattlePoint;
    public Transform enemyBattlePoint;

    public static BattleState state;

    public Unit playerUnit;
    public Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Animator enemyAnimator;
    public Animator playerAnimator;
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }
    IEnumerator SetupBattle()
    {
        GameObject PlayerGo = Instantiate(playerPrefab, playerBattlePoint);
        playerUnit = PlayerGo.GetComponent<Unit>();

        GameObject EnemyGo = Instantiate(enemyPrefab, enemyBattlePoint);
        enemyUnit = EnemyGo.GetComponent<Unit>();

        var enemySO = BattleManager.Instance.enemySO;

        enemyAnimator = EnemyGo.GetComponentInChildren<Animator>();
        enemyAnimator.runtimeAnimatorController = enemySO.anim;
        playerAnimator = PlayerGo.GetComponentInChildren<Animator>();

        enemyUnit.unitID = enemySO.unitID;
        enemyUnit.portrait = enemySO.portrait;
        enemyUnit.unitLevel = enemySO.unitLevel;
        enemyUnit.maxHP = enemySO.maxHP;
        enemyUnit.currentHP = enemySO.maxHP;
        enemyUnit.damage = enemySO.damage;
        enemyUnit.defense = enemySO.defense;
        enemyUnit.exp = enemySO.exp;

        dialogueText.text = "O " + enemyUnit.unitID + " Se aproximou!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit);
        playerAnimator.SetTrigger("Attack");
        dialogueText.text = "O " + enemyUnit.unitID + " recebeu " + playerUnit.damage + " de dano!";
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(1.5f);
        enemyAnimator.SetTrigger("Hurt");
        yield return new WaitForSeconds(1.5f);
        
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
        
    }

    IEnumerator EnemyTurn()
    {
        actionsCanvas.SetActive(false);
        dialogueText.text = enemyUnit.unitID + " Ataca!";
        enemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit);
        dialogueText.text = "O " + playerUnit.unitID + " recebeu " + enemyUnit.damage + " de dano!";
        playerAnimator.SetTrigger("Hurt");
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        
    }

    IEnumerator WonBattle()
    {
        dialogueText.text = "Voce venceu a batalha!";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Mundo");
    }
    IEnumerator LostBattle()
    {
        dialogueText.text = "Voce Perdeu a batalha!";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Menu");
    }

    void PlayerTurn()
    {
        dialogueText.text = "Escolha uma ação:";
        actionsCanvas.SetActive(true);
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    public void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Voce venceu a batalha!";
            StartCoroutine(WonBattle());
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Voce foi derrotado!";
            StartCoroutine(LostBattle());
        }
    }
}
