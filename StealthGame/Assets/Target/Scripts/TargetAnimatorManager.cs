using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimatorManager : AnimatorManager
{
    TargetManager TargetManager;

    protected override void Awake()
    {
        base.Awake();
        TargetManager = GetComponent<TargetManager>();
    }
}
