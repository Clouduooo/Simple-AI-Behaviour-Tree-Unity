using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Animator _animator;
    private Transform[] _pathPoints;

    private int _currentPathPointIndex = 0;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] pathPoints)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _pathPoints = pathPoints;
    }
    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
                _animator.SetBool("Walking", true);
            }
        }
        else
        {
            Transform pathpoint = _pathPoints[_currentPathPointIndex];
            if (Vector3.Distance(_transform.position, pathpoint.position) < 0.01f)
            {
                _transform.position = pathpoint.position;
                _waitCounter = 0f;
                _waiting = true;
                _currentPathPointIndex = (_currentPathPointIndex + 1) % _pathPoints.Length;
                _animator.SetBool("Walking", false);
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, pathpoint.position, GuardBT.speed * Time.deltaTime);
                _transform.LookAt(pathpoint.position);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
