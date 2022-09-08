using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hoffman
{
    public class HoffmanTree
    {
        private List<Node> nodes = new List<Node>();
        private Node root;
        private Dictionary<int, int> fraqs = new Dictionary<int, int>();

        public void Build(int[] source)
        {
            for (int i = 0; i < source.Length; ++i)
            {
                if (!fraqs.ContainsKey(source[i]))
                {
                    fraqs.Add(source[i],0);
                }

                ++fraqs[source[i]];
            }
            foreach (KeyValuePair<int,int> v in fraqs)
            {
                nodes.Add(new Node(){Symbol = v.Key,Fraq = v.Value});
            }
            while (nodes.Count>1)
            {
                List<Node> orderednodes = nodes.OrderBy(node => node.Fraq).ToList<Node>();
                if (orderednodes.Count >= 2)
                {
                    List<Node> take = orderednodes.Take(2).ToList();
                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Fraq = take[0].Fraq + take[1].Fraq,
                        left = take[0],
                        right = take[1]
                    };
                    nodes.Remove(take[0]);
                    nodes.Remove(take[1]);
                    nodes.Add(parent);
                }
                this.root = nodes.FirstOrDefault();
            }
        }

        public BitArray encode(int[] source)
        {
            List<bool> infodecode = new List<bool>();
            for (int i = 0; i < source.Length; ++i)
            {
                List<bool> encsymb = root.Travestal(source[i], new List<bool>());
                infodecode.AddRange(encsymb);
            }
            return new BitArray(infodecode.ToArray());
        }

        public string decoded(BitArray bits)
        {
            Node current = root;
            string decode = "";
            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.right != null)
                    {
                        current = current.right;
                    }
                }
                else
                {
                    if (current.left != null)
                    {
                        current = current.left;
                    }
                }
                if (current.right == null && current.left == null)
                {
                    decode += current.Symbol + " ";
                    current = root;
                }
            }
            return decode;
        }
        
    }
}