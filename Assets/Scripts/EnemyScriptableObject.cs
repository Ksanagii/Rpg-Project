// UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public string unitID;
    public Sprite portrait;
    public RuntimeAnimatorController anim;
    public int unitLevel;
    public int maxHP;
    public int damage;
    public int defense;
    public int exp;
}
