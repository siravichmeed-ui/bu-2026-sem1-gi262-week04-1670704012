using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Assignment
{
    public class Lecture : MonoBehaviour
    {
        public void Start()
        {
            // LCT01_SyntaxList();
            // LCT02_SyntaxLinkedList();
            // LCT03_SyntaxHashTable();
            // LCT04_SyntaxDictionary();
        }

        #region Lecture

        public void LCT01_SyntaxList()
        {
            List<string> lists = new List<string>();
        }

        public void LCT02_SyntaxLinkedList()
        {
            LinkedList<string> linkedList = new LinkedList<string>();
            //Node 1 
            linkedList.AddLast("Node 1");
            //Node 1 // -> Node 2
            linkedList.AddLast("Node 2");
            //Node 0 -> Node 1 -> Node 2
            linkedList.AddFirst("Node 0");

            LinkedListNode<string> firstNode = linkedList.First;
            //string firstNode2 = linkedList.First;
            Debug.Log("first:" + firstNode.Value);
            LinkedListNode<string> lastNode = linkedList.Last;
            Debug.Log("last" + lastNode.Value);
            LinkedListNode<string> node1 = linkedList.Find("Node 1");
            Debug.Log("node: " + node1.Value);
            Debug.Log(node1.Previous.Value);
            Debug.Log(node1.Next.Value);
            //Debug.Log(node1.Next.Next.Next.Next.Next)
            if (firstNode.Previous == null)
            {
                Debug.Log("firstNode.Previous is null");
            }
            if (lastNode.Previous == null)
            {
                Debug.Log("lastNode.Next is null");
            }

            linkedList.AddAfter(node1, "Node 1.5");
            linkedList.AddBefore(node1, "Node 0.5");
            PrintLinkList(linkedList);

            linkedList.RemoveFirst();
            PrintLinkList(linkedList);
            linkedList.Remove("Node 2");
            PrintLinkList(linkedList);
            linkedList.Clear();
            PrintLinkList(linkedList);

        }
        void PrintLinkList(LinkedList<string> linkedList)
        {
            Debug.Log("=======linkedList=======");
            foreach (string s in linkedList)
            {
                Debug.Log(s);
            }
        }

        public void LCT03_SyntaxHashTable()
        {
            throw new System.NotImplementedException();
        }

        public void LCT04_SyntaxDictionary()
        {
            Dictionary<int, string> directionary = new Dictionary<int, string>();
            directionary.Add(1, "Apple");
            directionary.Add(2, "Banana");
            directionary[3] = "Cherry";

            int keytocheck = 1;
            bool hasKey = directionary.ContainsKey(keytocheck);
            Debug.Log($"has key {keytocheck}:{hasKey}");
            if (hasKey)
            {
                Debug.Log(directionary[keytocheck]);
            }

            foreach (int x in directionary.Keys)
            {
                Debug.Log(x);
            }
        }

        #endregion
    }
}
