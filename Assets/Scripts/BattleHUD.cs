using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitID;
        levelText.text = "Lvl " + unit.unitLevel;
        hpText.maxValue = unit.maxHP;
        hpText.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpText.value = hp;
    }
}
