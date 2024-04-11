using System.Collections.Generic;
using BehaviourTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] pathPoints;

    public static float speed = 2f;
    public static float fovRange = 4f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform)
            }),
            new TaskPatrol(transform, pathPoints)
        });

        return root;
    }
}
