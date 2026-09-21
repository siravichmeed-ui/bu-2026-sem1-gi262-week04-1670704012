using UnityEngine;
using System.Collections.Generic;

namespace Assignment
{
    public class Assignment : MonoBehaviour
    {
        public void Start()
        {
            //AS01_CountWords();
            AS02_CountNumber();
            AS03_CheckValidBrackets();
            AS04_PrintReverseLinkedList();
            AS05_FindMiddleElement();
            AS06_MergeDictionaries();
            AS07_RemoveDuplicatesFromLinkedList();
            AS08_TopFrequentNumber();
            AS09_PlayerInventory();
            AS10_GameEventQueue();
            AS11_PlayerStatsTracker();
        }

        #region Assignment

        [Header("AS01 - Count Words")]
        [SerializeField] private string[] as01Words;

        public void AS01_CountWords()
        {
            string[] words = as01Words;

            
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount.Add(word, 1);
                }
            }

            
            string[] keys = new string[wordCount.Count];
            int[] values = new int[wordCount.Count];

            wordCount.Keys.CopyTo(keys, 0);
            wordCount.Values.CopyTo(values, 0);

            
            for (int i = 0; i < keys.Length; i++)
            {
                Debug.Log("word: '" + keys[i] + "' count: " + values[i]);
            }
        }
        

        [Header("AS02 - Count Number")]
        [SerializeField] private int[] as02Numbers;

        public void AS02_CountNumber()
        {
            int[] numbers = as02Numbers;

            
            Dictionary<int, int> numberCount = new Dictionary<int, int>();

            
            for (int i = 0; i < numbers.Length; i++)
            {
                int number = numbers[i];

                
                if (numberCount.ContainsKey(number))
                {
                    numberCount[number]++;
                }
                else
                {
                    numberCount.Add(number, 1);
                }
            }

            
            int[] keys = new int[numberCount.Count];
            int[] values = new int[numberCount.Count];

            numberCount.Keys.CopyTo(keys, 0);
            numberCount.Values.CopyTo(values, 0);

            
            for (int i = 0; i < keys.Length; i++)
            {
                Debug.Log("number: " + keys[i] + " count: " + values[i]);
            }
        }

        [Header("AS03 - Check Valid Brackets")]
        [SerializeField] private string as03Input;

        public void AS03_CheckValidBrackets()
        {
            string input = as03Input;

            
            Dictionary<char, char> brackets = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' }
        };

            
            LinkedList<char> stack = new LinkedList<char>();

            
            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];

                
                if (brackets.ContainsKey(current))
                {
                    stack.AddLast(current);
                }
                
                else if (current == ')' || current == ']' || current == '}')
                {
                    
                    if (stack.Count == 0)
                    {
                        Debug.Log("Invalid");
                        return;
                    }

                    
                    char lastOpenBracket = stack.Last.Value;

                    
                    if (brackets[lastOpenBracket] != current)
                    {
                        Debug.Log("Invalid");
                        return;
                    }

                    
                    stack.RemoveLast();
                }

                
            }


            if (stack.Count == 0)
            {
                Debug.Log("Valid");
            }
            else
            {
                Debug.Log("Invalid");
            }
        }

        [Header("AS04 - Print Reverse Linked List")]
        [SerializeField] private IntLinkedListInput as04List = new IntLinkedListInput();

        public void AS04_PrintReverseLinkedList()
        {
            LinkedList<int> list = as04List.GetLinkedList();

            if (list.Count == 0)
            {
                Debug.Log("List is empty");
                return;
            }


            LinkedListNode<int> current = list.Last;


            while (current != null)
            {
                Debug.Log(current.Value);

                current = current.Previous;
            }
        }

        [Header("AS05 - Find Middle Element")]
        [SerializeField] private StringLinkedListInput as05List = new StringLinkedListInput();

        public void AS05_FindMiddleElement()
        {
            LinkedList<string> list = as05List.GetLinkedList();

            
            if (list.Count == 0)
            {
                Debug.Log("List is empty");
                return;
            }


            LinkedListNode<string> slow = list.First;
            LinkedListNode<string> fast = list.First;


            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }


            Debug.Log(slow.Value);
        }

        [Header("AS06 - Merge Dictionaries")]
        [SerializeField] private StringIntDictionaryInput as06FirstDictionary = new StringIntDictionaryInput();
        [SerializeField] private StringIntDictionaryInput as06SecondDictionary = new StringIntDictionaryInput();

        public void AS06_MergeDictionaries()
        {
            Dictionary<string, int> dict1 = as06FirstDictionary.GetDictionary();
            Dictionary<string, int> dict2 = as06SecondDictionary.GetDictionary();


            Dictionary<string, int> mergedDictionary =
                new Dictionary<string, int>(dict1);


            foreach (KeyValuePair<string, int> pair in dict2)
            {
                string key = pair.Key;
                int value = pair.Value;


                if (mergedDictionary.ContainsKey(key))
                {
                    mergedDictionary[key] += value;
                }
                else
                {
                    mergedDictionary.Add(key, value);
                }
            }

            // แสดงผล
            foreach (KeyValuePair<string, int> pair in mergedDictionary)
            {
                Debug.Log("key: " + pair.Key + ", value: " + pair.Value);
            }
        }
        

        [Header("AS07 - Remove Duplicates From Linked List")]
        [SerializeField] private IntLinkedListInput as07List = new IntLinkedListInput();

        public void AS07_RemoveDuplicatesFromLinkedList()
        {
            LinkedList<int> list = as07List.GetLinkedList();


            if (list.Count <= 1)
            {
                LinkedListNode<int> node = list.First;

                while (node != null)
                {
                    Debug.Log(node.Value);
                    node = node.Next;
                }

                return;
            }

            
            Dictionary<int, bool> seen = new Dictionary<int, bool>();

            
            LinkedListNode<int> current = list.First;

            while (current != null)
            {
                
                LinkedListNode<int> next = current.Next;

                
                if (seen.ContainsKey(current.Value))
                {
                    list.Remove(current);
                }
                else
                {
                    seen.Add(current.Value, true);
                }

                
                current = next;
            }

            
            LinkedListNode<int> result = list.First;

            while (result != null)
            {
                Debug.Log(result.Value);
                result = result.Next;
            }
        }

        [Header("AS08 - Top Frequent Number")]
        [SerializeField] private int[] as08Numbers;

        public void AS08_TopFrequentNumber()
        {
            int[] numbers = as08Numbers;

            
            if (numbers == null || numbers.Length == 0)
            {
                Debug.Log("Input array is empty");
                return;
            }

            
            Dictionary<int, int> numberCount = new Dictionary<int, int>();

            
            for (int i = 0; i < numbers.Length; i++)
            {
                int number = numbers[i];

                if (numberCount.ContainsKey(number))
                {
                    numberCount[number]++;
                }
                else
                {
                    numberCount.Add(number, 1);
                }
            }

            
            int topNumber = numbers[0];
            int maxCount = numberCount[numbers[0]];

            
            for (int i = 0; i < numbers.Length; i++)
            {
                int number = numbers[i];
                int currentCount = numberCount[number];

                if (currentCount > maxCount)
                {
                    topNumber = number;
                    maxCount = currentCount;
                }
            }

            
            Debug.Log(topNumber + " count: " + maxCount);
        }
        

        [Header("AS09 - Player Inventory")]
        [SerializeField] private StringIntDictionaryInput as09Inventory = new StringIntDictionaryInput();
        [SerializeField] private string as09ItemName;
        [SerializeField] private int as09Quantity;

        public void AS09_PlayerInventory()
        {
            Dictionary<string, int> inventory = as09Inventory.GetDictionary();
            string itemName = as09ItemName;
            int quantity = as09Quantity;

            
            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName] += quantity;
            }
            else
            {
                inventory.Add(itemName, quantity);
            }

            
            foreach (KeyValuePair<string, int> item in inventory)
            {
                Debug.Log(item.Key + ": " + item.Value);
            }
        }

        [Header("AS10 - Game Event Queue")]
        [SerializeField] private GameEventLinkedListInput as10EventQueue = new GameEventLinkedListInput();

        public void AS10_GameEventQueue()
        {
            LinkedList<GameEvent> eventQueue = as10EventQueue.GetLinkedList();

            
            if (eventQueue.Count == 0)
            {
                Debug.Log("Event queue is empty");
                return;
            }

           
            while (eventQueue.Count > 0)
            {
                
                GameEvent currentEvent = eventQueue.First.Value;

                
                eventQueue.RemoveFirst();

                
                Debug.Log("Processing event: " + currentEvent.Name);

               
                Debug.Log("Remaining events in queue: " + eventQueue.Count);

                
                if (currentEvent.EventType == "enemy")
                {
                    Debug.Log("Enemy event processed - " + currentEvent.Name);
                }
                else if (currentEvent.EventType == "powerup")
                {
                    Debug.Log("Power-up event processed - " + currentEvent.Name);
                }
                else if (currentEvent.EventType == "level")
                {
                    Debug.Log("Level event processed - " + currentEvent.Name);
                }
            }
        }

        [Header("AS11 - Player Stats Tracker")]
        [SerializeField] private StringIntDictionaryInput as11PlayerStats = new StringIntDictionaryInput();
        [SerializeField] private string as11StatName;
        [SerializeField] private int as11Value;

        public void AS11_PlayerStatsTracker()
        {
            Dictionary<string, int> playerStats = as11PlayerStats.GetDictionary();
            string statName = as11StatName;
            int value = as11Value;

            if (playerStats.ContainsKey(statName))
            {
                playerStats[statName] += value;
            }
            else
            {
                playerStats.Add(statName, value);
            }

            // ต้องแสดง Updated ก่อน
            Debug.Log("Updated " + statName + ": " + playerStats[statName]);

            // แล้วแสดงหัวข้อ
            Debug.Log("Current player statistics:");

            // แสดงข้อมูลทั้งหมด
            foreach (KeyValuePair<string, int> stat in playerStats)
            {
                Debug.Log(stat.Key + ": " + stat.Value);
            }
        }

        #endregion
    }
}
