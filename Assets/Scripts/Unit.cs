using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitID;
    public Sprite portrait;
    public int unitLevel;
    public int maxHP;
    public int currentHP;
    public int damage;
    public int defense;
    public int exp;



    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            return true;
        }
        return false;
    }
}
