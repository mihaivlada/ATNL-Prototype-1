using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerTransform;
    private float _movementSpeed = 1.75f;

    private float _exitTimer;
    private float _timeTillExit = 3f;
    private float _distanceToCountExit = 3f;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy is now in Attack State");

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Vector3 moveDirection = (_playerTransform.position - enemy.transform.position).normalized;

        enemy.MoveEnemy(moveDirection * _movementSpeed);

        if (Vector3.Distance(enemy.transform.position, _playerTransform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer >= _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
                _exitTimer = 0f;
            }
        }
        else
        {
            _exitTimer = 0f;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
