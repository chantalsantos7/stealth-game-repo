public class EnemyAnimatorManager : AnimatorManager
{
    EnemyManager enemyManager;

    protected override void Awake()
    {
        base.Awake();
        enemyManager = GetComponent<EnemyManager>();
    }
}
