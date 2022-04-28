using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure
{
    public class Trie
    {
        private class Node
        {
            private Node[] nodes = new Node[26];
            private bool terminal = false;

            public bool Contains(char c) => nodes[c - 'a'] != null;
            public void InsertNode(char c, Node node)
            {
                nodes[c - 'a'] = node;
            }
            public void SetTerminal() => terminal = true;
            public Node GetNode(char c) => nodes[c - 'a'];
            public bool isTerminal() => terminal;
        }

        private Node root;

        private void CollectWords(Node root, string word, ref List<string>res)
        {
            char c = 'a';
            for(int i=0; i<26; i++)
            {
                if(i > 0) { c = (char)('a' + i); }

                if(root.Contains(c))
                {
                    word += c;
                    CollectWords(root.GetNode(c), word, ref res);
                    word = word.Remove(word.Length - 1);
                }
            }

            if (root.isTerminal()) { res.Add(word); }
        }

        public Trie() { root = new Node(); }

        public void Insert(string word)
        {
            var node = root;
            for (int i = 0; i < word.Length; i++)
            {
                char this_char = word[i];
                if(Char.IsDigit(this_char))
                {
                    int n = Convert.ToInt32(new string(this_char, 1));
                    if (n > 0)
                    {
                        this_char = (char)('a' + (n - 1));
                    }
                    else { this_char = 'a'; }
                }

                if (!node.Contains(this_char)) { node.InsertNode(this_char, new Node()); }
                node = node.GetNode(this_char);
            }

            node.SetTerminal();
        }

        public bool FindPrefix(string prefix)
        {
            var node = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                char this_char = prefix[i];
                if (Char.IsDigit(this_char))
                {
                    int n = Convert.ToInt32(new string(this_char, 1));
                    this_char = (char)('a' + (n - 1));
                }
                if (!node.Contains(this_char)) { return false; }
                node = node.GetNode(this_char);
            }

            return true;
        }

        public bool Search(string word)
        {
            var node = root;
            for (int i = 0; i < word.Length; i++)
            {
                char this_char = word[i];
                if (Char.IsDigit(this_char))
                {
                    int n = Convert.ToInt32(new string(this_char, 1));
                    this_char = (char)('a' + (n - 1));
                }
                if (!node.Contains(this_char)) { return false; }
                node = node.GetNode(this_char);
            }

            return node.isTerminal();
        }

        public List<string> FindAllPrefixes(string prefix)
        {
            List<string> matches = new List<string>();
            var node = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                char this_char = prefix[i];
                if (Char.IsDigit(this_char))
                {
                    int n = Convert.ToInt32(new string(this_char, 1));
                    this_char = (char)('a' + (n - 1));
                }
                if (!node.Contains(this_char)) { return null; }
                node = node.GetNode(this_char);
            }

            CollectWords(node, prefix, ref matches);

            return matches;
        }
    }
}
