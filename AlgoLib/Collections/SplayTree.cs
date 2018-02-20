using System;

namespace AlgoLib.Collections
{
    public class SplayTree<T>
    {
        private class SplayTreeNode
        {
            public T Value { get; }

            public SplayTreeNode Left { get; set; }
            public SplayTreeNode Right { get; set; }
            public SplayTreeNode Parent { get; set; }

            public SplayTreeNode(T value)
            {
                Value = value;
            }
        }

        private readonly Comparison<T> _comparison;
        private SplayTreeNode _root;

        public SplayTree(Comparison<T> comparison)
        {
            _comparison = comparison;
        }

        private SplayTree(Comparison<T> comparison, SplayTreeNode root)
        {
            _root = root;
            _comparison = comparison;
        }

        public void Add(T value)
        {
            if (_root == null)
            {
                _root = new SplayTreeNode(value);
                return;
            }

            SplayTreeNode closest = FindClosest(value);

            if (_comparison(closest.Value, value) == 0)
            {
                Splay(closest);
                _root = closest;
                return;
            }

            SplayTreeNode newNode = AddChild(closest, value);
            Splay(newNode);
            _root = newNode;
        }

        public bool Contains(T value)
        {
            SplayTreeNode closest = FindClosest(value);
            if (closest == null)
            {
                return false;
            }

            Splay(closest);
            _root = closest;
            return _comparison(closest.Value, value) == 0;
        }


        public void Delete(T value)
        {
            SplayTreeNode node = FindClosest(value);

            Splay(node);
            _root = node;
            if (node != null && _comparison(node.Value, value) == 0)
            {
                if (node.Left == null)
                {
                    _root = node.Right;
                    if (_root != null)
                    {
                        _root.Parent = null;
                    }

                    return;
                }

                if (node.Right == null)
                {
                    _root = node.Left;
                    if (_root != null)
                    {
                        _root.Parent = null;
                    }

                    return;
                }

                node.Left.Parent = null;
                node.Right.Parent = null;
                _root = node.Left;
                Merge(node.Right);
            }
        }

        public (SplayTree<T> Left, SplayTree<T> Right) Split(T value)
        {
            SplayTreeNode root = FindClosest(value);
            Splay(root);
            _root = root;

            if (root == null)
            {
                return (null, null);
            }

            int comparison = _comparison(root.Value, value);
            if (comparison >= 0)
            {
                SplayTreeNode left = root.Left;
                root.Left = null;
                if (left != null)
                {
                    left.Parent = null;
                }

                return (new SplayTree<T>(_comparison, left), new SplayTree<T>(_comparison, root));
            }

            if (comparison < 0)
            {
                SplayTreeNode right = root.Right;
                root.Right = null;
                if (right != null)
                {
                    right.Parent = null;
                }

                return (new SplayTree<T>(_comparison, root), new SplayTree<T>(_comparison, right));
            }

            if (root.Left != null)
            {
                root.Left.Parent = null;
            }

            if (root.Right != null)
            {
                root.Right.Parent = null;
            }

            return (new SplayTree<T>(_comparison, root.Left), new SplayTree<T>(_comparison, root.Right));
        }

        public void Merge(SplayTree<T> other) => Merge(other._root);

        private void Merge(SplayTreeNode node)
        {
            if (node == null) return;

            if (_root == null)
            {
                _root = node;
                return;
            }

            SplayTreeNode current = _root;
            while (current.Right != null)
            {
                current = current.Right;
            }

            Splay(current);
            _root = current;
            current.Right = node;
            node.Parent = current;
        }

        private SplayTreeNode AddChild(SplayTreeNode node, T value)
        {
            var newNode = new SplayTreeNode(value);
            if (_comparison(node.Value, value) > 0)
            {
                node.Left = newNode;
            }
            else
            {
                node.Right = newNode;
            }

            newNode.Parent = node;
            return newNode;
        }

        private SplayTreeNode FindClosest(T value)
        {
            SplayTreeNode parent = _root;
            SplayTreeNode current = _root;
            while (current != null)
            {
                parent = current;
                if (_comparison(current.Value, value) > 0)
                {
                    current = current.Left;
                }
                else if (_comparison(current.Value, value) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    return current;
                }
            }

            return parent;
        }

        private void Splay(SplayTreeNode node)
        {
            while (node != null && node.Parent != null)
            {
                SplayTreeNode parent = node.Parent;
                SplayTreeNode grandParent = parent.Parent;
                if (grandParent == null)
                {
                    if (parent.Left == node)
                    {
                        RotateRight(parent);
                    }
                    else
                    {
                        RotateLeft(parent);
                    }
                }
                else
                {
                    if (grandParent.Left == parent && parent.Left == node)
                    {
                        RotateRight(grandParent);
                        RotateRight(parent);
                    }
                    else if (grandParent.Right == parent && parent.Right == node)
                    {
                        RotateLeft(grandParent);
                        RotateLeft(parent);
                    }
                    else if (grandParent.Left == parent && parent.Right == node)
                    {
                        RotateLeft(parent);
                        RotateRight(grandParent);
                    }
                    else if (grandParent.Right == parent && parent.Left == node)
                    {
                        RotateRight(parent);
                        RotateLeft(grandParent);
                    }
                }
            }
        }

        private void RotateRight(SplayTreeNode x)
        {
            SplayTreeNode xParent = x.Parent;
            SplayTreeNode y = x.Left;
            SplayTreeNode yRight = y?.Right;

            x.Parent = y;
            x.Left = yRight;


            if (y != null)
            {
                y.Right = x;
                y.Parent = xParent;

                if (yRight != null)
                {
                    yRight.Parent = x;
                }
            }


            if (xParent != null)
            {
                if (xParent.Left == x)
                {
                    xParent.Left = y;
                }
                else
                {
                    xParent.Right = y;
                }
            }
        }

        private void RotateLeft(SplayTreeNode x)
        {
            SplayTreeNode xParent = x.Parent;
            SplayTreeNode y = x.Right;
            SplayTreeNode yLeft = y.Left;

            x.Parent = y;
            x.Right = yLeft;

            if (y != null)
            {
                y.Left = x;
                y.Parent = xParent;

                if (yLeft != null)
                {
                    yLeft.Parent = x;
                }
            }

            if (xParent != null)
            {
                if (xParent.Left == x)
                {
                    xParent.Left = y;
                }
                else
                {
                    xParent.Right = y;
                }
            }
        }
    }
}