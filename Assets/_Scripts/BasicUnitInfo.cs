using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicUnitInfo : MonoBehaviour, IBeAtacked
{
    [SerializeField]
    protected int _maxHealth;

    [SerializeField]
    protected int _curHealth;

    public virtual void Start()
    {
        _curHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _curHealth -= damage;
    }


}
