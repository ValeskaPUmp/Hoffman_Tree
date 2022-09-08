using System;
using System.Collections.Generic;

namespace Hoffman
{
    public class Node
    {
        public Node right;
        public Node left;
        public int Fraq;
        public int Symbol;

        public List<bool> Travestal(int symbol,List<bool> data)
        {
            if (right == null && left == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    return data;
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                List<bool> Right = null;
                List<bool> Left = null;
                if (left != null)
                {
                    List<bool> leftpath = new List<bool>();
                    leftpath.AddRange(data);
                    leftpath.Add(false);
                    Left = left.Travestal(symbol, leftpath);
                }

                if (right != null)
                {
                    List<bool> rightpath = new List<bool>();
                    rightpath.AddRange(data);
                    rightpath.Add(true);
                    Right = right.Travestal(symbol, rightpath);
                }

                if (Left != null)
                {
                    return Left;
                }
                else
                {
                    return Right;
                }
            }
            

        }


    }
}