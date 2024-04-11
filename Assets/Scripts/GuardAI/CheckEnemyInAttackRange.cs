using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyInAttackRange : Node
{
    Transform _transform;
    Animator _animator;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        
        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange)
        {
            state = NodeState.SUCCESS;
            _animator.SetBool("Attacking", true);
            _animator.SetBool("Running", false);
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
