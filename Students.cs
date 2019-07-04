using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> tree = new BinaryTree<int>(1);
            Console.WriteLine("Введіть початкову кількість елементів бінарного дерева: ");
            int.TryParse(Console.ReadLine(), out int Count);
            Console.WriteLine("Введіть корінь бінарного дерева: ");
            int.TryParse(Console.ReadLine(), out int root);
            tree.Add(root);
            int number;
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine("Введіть число:");
                int.TryParse(Console.ReadLine(), out number);
                tree.Add(number);
            }
            int menu;
            do
            {
                Console.Clear();
                Console.WriteLine("МЕНЮ: \n1 - Показати дерево. \n2 - Додати елемент. \n3 - Видалити елемент. " +
                "\n4 - Видалити дерево. \n5 - Показати кількість элементів бінарного дерева. \n6 - Показати глубину заданого елемента бінарного дерева. \nОберіть: ");
                int.TryParse(Console.ReadLine(), out menu);
                if (menu > 0 && menu < 7)
                {
                    switch (menu)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Console.WriteLine("Preorder");
                                Console.WriteLine(tree.Preorder());
                                Console.ReadKey();
                            }
                            break;
                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("Введіть кількість  елементів що потрібно додати: ");
                                int.TryParse(Console.ReadLine(), out int newelem);
                                for (int i = 0; i < newelem; i++)
                                {
                                    Console.WriteLine("Введіть число:");
                                    int.TryParse(Console.ReadLine(), out number);
                                    tree.Add(number);
                                }
                                Console.WriteLine("Числа додані!");
                            }
                            break;
                        case 3:
                            {
                                Console.Clear();
                                Console.WriteLine("Введіть елемент для видалення: ");
                                int.TryParse(Console.ReadLine(), out int value);
                                tree.Remove(value);
                                Console.WriteLine("Елемент видалено!");
                                Console.WriteLine(tree.Preorder());
                                Console.ReadKey();
                            }
                            break;
                        case 4:
                            {
                                Console.Clear();
                                tree.Clear();
                                Console.WriteLine("Дерево видалено!");
                                Console.ReadKey();
                            }
                            break;
                        case 5:
                            {
                                Console.Clear();
                                Console.WriteLine($"Кількість елементів бінарного дерева: {tree.Count}");
                                Console.ReadKey();
                            }
                            break;
                        case 6:
                            {
                                Console.Clear();
                                Console.WriteLine("Введіть елемент, для якого потрібно знайти глубину: ");
                                int.TryParse(Console.ReadLine(), out int value);
                                Console.WriteLine(tree.DepthTree(value, tree));
                                Console.ReadKey();
                            }
                            break;
                    }
                }
            }
            while (menu != 0);
        }


    }
    class BinaryTree<T> where T : IComparable<T>
    {
        static int level = 0;
        private BinaryTreeNode<T> root; // посилання на корінь дерева
        public int Count { get; set; }

        public delegate void NewNodeAddedHandler(string msg);
        //подія додавання вузла 
        public event NewNodeAddedHandler Added;

        public BinaryTree(T value)
        {
            level = 0;
            root = new BinaryTreeNode<T>(value, ++level);
        }
        public void Add(T value)
        {
            level = 0;
            if (root == null)
            { root = new BinaryTreeNode<T>(value, ++level); }
            else
            {
                AddTo(root, value, ++level);
                if (Added != null)
                    Added($"Було додадо нову гілку дерева!");
            }
            Count++;
        }
        private void AddTo(BinaryTreeNode<T> node, T value, int lvl)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                    node.Left = new BinaryTreeNode<T>(value, ++lvl);
                else
                    AddTo(node.Left, value, ++lvl);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new BinaryTreeNode<T>(value, ++lvl);
                else
                    AddTo(node.Right, value, ++lvl);
            }
        }
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current;
            current = FindWithParent(value, out BinaryTreeNode<T> parent, this);
            if (current == null)
            {
                return false;
            }
            Count--;
            if (current.Right == null)//якщо правий нащадок пустий
            {
                if (parent == null)//якщо батько пустий
                {
                    root = current.Left;//корнем стає лівий син
                }
                else
                {
                    int result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // Якщо значення батька більше поточного,
                        // лівий син поточного вузла стає лівим сином батька.
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    { // Якщо значення батька менше поточного,
                        // лівий син поточного вузла стає правим сином батька. 
                        parent.Right = current.Left;
                    }
                }
            }
            // Випадок 2: Якщо у правого сина немає дітей зліва // то він займає місце вузла що видаляється. 
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    root = current.Right;
                }
                else
                {
                    int result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // Якщо значення батька більше поточного,
                        // правий син поточного вузла стає лівим сином батька.

                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        // Якщо значення батька менше поточного, 
                        // правий син поточного вузла стає правим сином батька. 
                        parent.Right = current.Right;
                    }
                }
            }
            // Випадок 3: Якщо у правого сина є сини зліва, крайній лівий син 
            // з правого піддерева заміняє вузол що видаляється. 
            else
            {
                // Знайдемо крайній лівий вузол. 
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;
                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost; leftmost = leftmost.Left;
                }
                // Ліве піддерево батька стає правим піддеревом крайнього лівого вузла. 
                leftmostParent.Left = leftmost.Right;
                // Лівий и правий син поточного вузла стає лівим и правим сином крайнього лівого.
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null)
                {
                    root = leftmost;
                }
                else
                {
                    int result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // Якщо значення батька більше поточного,
                        // крайній лівий вузол стає лівим сином батька.
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        // Якщо значення батька менше поточного,
                        // крайній лівий вузол стає правим сином батька.
                        parent.Right = leftmost;
                    }
                }
            }
            return true;
        }
        public bool Contains(T value, BinaryTree<T> tree)
        {
            return FindWithParent(value, out BinaryTreeNode<T> parent, tree) != null;
        }

        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent, BinaryTree<T> tree)
        {
            BinaryTreeNode<T> current = tree.root;
            parent = null;
            do
            {
                int result = current.Value.CompareTo(value);

                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            while (current != null);
            return current;
        }
        public void Clear()
        {
            root = null;
            Count = 0;
        }

        public int DepthTree(T val, BinaryTree<T> tree)
        {
            if (tree.Contains(val, tree)) // якщо поточний вузол не порожній
            {
                return FindWithParent(val, out BinaryTreeNode<T> parent, tree).Level - 1;
            }
            return -1;
        }
        public IEnumerable<T> Preorder()
        {
            if (root == null) yield break;

            var stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node.Value;
                if (node.Right != null) stack.Push(node.Right);
                if (node.Left != null) stack.Push(node.Left);
            }
        }
        
    }
    class BinaryTreeNode<T> where T : IComparable<T>
    {
        public BinaryTreeNode(T value, int lvl)
        {
            Value = value;
            Level = lvl;
        }
        public int Level { get; set; }
        public BinaryTreeNode<T> Left { get; set; }//посилання на лівого сина
        public BinaryTreeNode<T> Right { get; set; }//посилання на правого сина
        public T Value { get; set; }//значення 
        public int CompareTo(BinaryTreeNode<T> other)
        {
            if (other == null) return 1;
            return Value.CompareTo(other.Value);
        }

    }
    class TestInfo : IComparable<TestInfo>
    {
        string StudentName { get; set; }
        string TestName { get; set; }
        DateTime PassDate { get; set; }
        double Grade { get; set; }

        public TestInfo(string studName, string testName, DateTime date, double mark)
        {
            StudentName = studName;
            TestName = testName;
            PassDate = date;
            Grade = mark;
        }

        public override string ToString()
        {
            return $"Test^{TestName}\tStudent:{StudentName}\tDate:{PassDate}\tGrade:{Grade}";
        }
        public int CompareTo(TestInfo other)
        {
            // If other is not a valid object reference, this instance is greater.
            if (other == null) return 1;

            // The test information comparison depends on the comparison of 
            // the grades values. 
            return Grade.CompareTo(other.Grade);
        }

        // Define the is greater than operator.
        public static bool operator >(TestInfo operand1, TestInfo operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        // Define the is less than operator.
        public static bool operator <(TestInfo operand1, TestInfo operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(TestInfo operand1, TestInfo operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(TestInfo operand1, TestInfo operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }
    }
}

