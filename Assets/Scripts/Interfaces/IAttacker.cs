using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker
{
    public Entity Target { get; set; }
    public int Damage { get; }
    public int AttackSpeed { get; }
    public int AttackDistance { get; }

    public void Attack();
}
