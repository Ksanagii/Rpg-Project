using System.Runtime.CompilerServices;
using UnityEngine;


public abstract class Base 
{
[SerializeField] private int Life = 100;
[SerializeField ]private int Attack = 25;
[SerializeField] private int Resistence = 10;
[SerializeField] private int ResistenceElemente = 0;

public void CollisionEnter2d(Collision2D collision)
{

}

}
