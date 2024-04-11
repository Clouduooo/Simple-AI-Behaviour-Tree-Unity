using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyInFOVRange : Node
{
    private static int _enemyLayerMask = 1 << 6;

    private Transform _transform;
    private Animator _animator;
    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, GuardBT.fovRange, _enemyLayerMask);
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetBool("Walking", false);
                _animator.SetBool("Running", true);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
