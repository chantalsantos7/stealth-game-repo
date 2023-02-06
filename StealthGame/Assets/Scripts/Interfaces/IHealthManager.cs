using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthManager
{
    public void DamageHealth(float amount);

    public void Die();

    public void SetRigidbodyState(bool state);

    public void SetColliderState(bool state);
}
