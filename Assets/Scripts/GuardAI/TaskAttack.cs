using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskAttack : Node
{
    private Animator _animator;
    private Transform _lastTarget;
    private EnemyManager _enemyManager;

    private float _attackCounter = 0f;
    private float _attackTime = 1f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _lastTarget = target;
            _enemyManager = target.GetComponent<EnemyManager>();
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool isEnemyDead = _enemyManager.TakeHit();
            if (isEnemyDead)
            {
                ClearData("target");
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Walking", true);
            }
            else
            {
                _attackCounter = 0f;
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
