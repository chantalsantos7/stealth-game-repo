using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public override void EnterState(EnemyStateManager context)
    {
        context.enemyInventory.sheathedSwordModel.SetActive(false);
        if (context.gameObject.CompareTag("Target"))
        {
            GameManager.Instance.achievementTracker.TargetIsDead = true;
        } 
        else
        {
            GameManager.Instance.achievementTracker.GuardsKilled++;
        }
        //context.enemyManager.isDead = true;
    }

    public override void UpdateState(EnemyStateManager context)
    {
       
    }

    public override void ExitState(EnemyStateManager context)
    {
        
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }

    
}
