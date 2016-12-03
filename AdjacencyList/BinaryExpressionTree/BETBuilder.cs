using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AdjacencyList.Graph;

namespace AdjacencyList.BinaryExpressionTree
{
    public class BETBuilder
    {
        private string _expression;
        public BETBuilder(string expression)
        {
            this._expression = expression;
        }

        private Stack<BNode> nodeStack = new Stack<BNode>(); 
        public void ConstructBTree(string expression, BNode root, int startIndex, int endIndex)
        {
            string symbol;
            var symbolIndex = FindHighestPriorityOperator(expression, out symbol);
            if (symbol == "+")
            {
                var s = new Stack<Object>();
                int index = 0;
                var nextSymbol = expression[index].ToString(CultureInfo.InvariantCulture);
               

                while (index <= endIndex)
                {
                    nextSymbol = expression[index].ToString(CultureInfo.InvariantCulture);
                    if (GetSymbolType(nextSymbol) == SymbolType.Digit)
                    {
                        var currentNode = new BNode()
                        {
                            Type = SymbolType.Digit,
                            Value = nextSymbol
                        };

                        if (!nodeStack.Any())
                        {
                            nodeStack.Push(currentNode);
                            continue;
                        }
                         var previousNode = nodeStack.Pop();
                        if (previousNode.Type == SymbolType.Operator)
                        {
                            previousNode.RightChild = currentNode;
                            nodeStack.Push(currentNode);
                        }
                    }
                    else if (GetSymbolType(nextSymbol) == SymbolType.Operator)
                    {
                        var previousNode = nodeStack.Pop();
                        if (previousNode.Type == SymbolType.Digit)
                        {
                            var currentNode = new BNode()
                            {
                                Type = SymbolType.Operator,
                                Value = nextSymbol,
                                LeftChild = previousNode
                            };
                            nodeStack.Push(currentNode);
                        }
                    }
                    nextSymbol = expression[index].ToString(CultureInfo.InvariantCulture);
                    s.Push(nextSymbol);
                    index++;
                }
            }

            ConstructBTree(expression, node, 0, index - 1);
            ConstructBTree(expression, node, index + 1, endIndex);
        }

        private SymbolType GetSymbolType(string currentSymbol)
        {
            int value;
            Debug.Assert(string.IsNullOrEmpty(currentSymbol), "Current symbol is either either null or Empty");
            if (int.TryParse(currentSymbol, out value))
            {
                return SymbolType.Digit;
            }
            return SymbolType.Operator;
        }

        private int FindHighestPriorityOperator(string expression, out string symbol)
        {
            if (expression.Length == 1)
            {
                symbol = string.Empty;
                return -1;
            }
            if (expression.Contains("*"))
            {
                symbol = "*";
                return expression.IndexOf("*", System.StringComparison.Ordinal);
            }
            else if (expression.Contains("/"))
            {
                symbol = "*";
                return expression.IndexOf("/", System.StringComparison.Ordinal);
            }
            else if (expression.Contains("+"))
            {
                symbol = "*";
                return expression.IndexOf("+", System.StringComparison.Ordinal);
            }
            else if (expression.Contains("-"))
            {
                symbol = "*";
                return expression.IndexOf("-", System.StringComparison.Ordinal);
            }
            else
            {
                symbol = "*";
                throw new InvalidExpressionException();
            }
        }
    }


    public class BNode
    {
        public SymbolType Type { get; set; }
        public string Value { get; set; }
        public BNode LeftChild { get; set; }
        public BNode RightChild { get; set; }
    }

    public enum SymbolType
    {
        Operator,
        Digit,
        None
    }

}
