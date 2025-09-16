using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpText;
    public TextMeshProUGUI hpTextValue;
    public Image portrait;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitID;
        hpTextValue.text = unit.currentHP.ToString() + "/ " + unit.maxHP.ToString();
        levelText.text = "Lvl " + unit.unitLevel;
        hpText.maxValue = unit.maxHP;
        hpText.value = unit.currentHP;
        if (unit.portrait != null)
        {
            portrait.sprite = unit.portrait;
        }
    }

    public void SetHP(Unit unit)
    {
        hpText.value = unit.currentHP;
        if(unit.currentHP < 0)
            unit.currentHP = 0;
        hpTextValue.text = unit.currentHP.ToString() + "/ " + unit.maxHP.ToString();
    }
}
