using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class TargetState
{
    public abstract void EnterState(TargetStateManager context);

    public abstract void ExitState(TargetStateManager context);

    public abstract void UpdateState(TargetStateManager context);

    public abstract void OnCollisionEnter(TargetStateManager context, Collision collision);
}
