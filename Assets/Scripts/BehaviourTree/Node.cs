using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE
    }

    public class Node
    {
        protected NodeState state;
        public Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }
        public Node(List<Node> childrenList)
        {
            foreach (Node child in childrenList)
            {
                _Attach(child);
            }
        }

        private void _Attach(Node node)
        {
            node.parent = this;
            this.children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object val)
        {
            _dataContext[key] = val;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node curNode = parent;
            while (curNode != null)
            {
                if (curNode._dataContext.TryGetValue(key, out value))
                    return value;
                curNode = curNode.parent;
            }
            return null;
        }
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node curNode = parent;
            while (curNode != null)
            {
                if (curNode._dataContext.ContainsKey(key))
                {
                    curNode._dataContext.Remove(key);
                    return true;
                }
                curNode = curNode.parent;
            }
            return false;
        }
    }
}