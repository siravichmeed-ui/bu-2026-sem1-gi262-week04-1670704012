using System.Collections.Generic;
using UnityEngine;

namespace Solution {
    public class Inventory : MonoBehaviour
    {
        public Dictionary<string, int> inventory = new Dictionary<string, int>();

        // เพิ่มไอเท็ม
        public void AddItem(string item, int amount)
        {
            // 1. ตรวจสอบว่ามีไอเท็มนี้ในคลังแล้วหรือยัง
            if (inventory.ContainsKey(item))
            {
                inventory[item] += amount;
            }
            else
            {
                inventory.Add(item, amount);
            }
            inventory.Add(item, amount);
            Debug.Log("Added " + amount + " " + item + ". Total: " + inventory[item]);
        }

        // ลบไอเท็ม
        public void RemoveItem(string item, int amount)
        {
            //4. ตรวจสอบว่ามีไอเท็มนี้ในคลังหรือไม่
            if (HasItem(item, amount))
            {
                inventory[item] -= amount;
                if (inventory[item] <= 0)
                {
                    inventory.Remove(item);
                    Debug.Log("Remove all" + item + "form inventory");
                }
                else
                {
                    Debug.Log("Remove" + amount + " " + item);
                }
            }
            else
            {
                Debug.Log("Can not remove item");
            }

        }
        public bool HasItem(string item, int amount)
        {
            //2. ตรวจสอบว่ามีไอเท็มนี้ในคลังหรือไม่ และมีจำนวนเพียงพอหรือไม่
            return inventory.ContainsKey(item) && inventory[item] >= amount;
        }
        // ตรวจสอบจำนวนไอเท็ม
        public int GetItemCount(string item)
        {
            //3. ตรวจสอบว่ามีไอเท็มนี้ในคลังหรือไม่ ถ้ามีให้คืนค่าจำนวนไอเท็มนั้น
            if (HasItem(item, 1))
            {
                return inventory[item];
            }
            return 0;
        }

        // แสดงรายการทั้งหมดในคลัง
        public void PrintInventory()
        {
            Debug.Log("--- Inventory Content ---");
            if (inventory.Count == 0)
            {
                Debug.Log("Inventory is empty.");
                return;
            }

            foreach (var itemEntry in inventory)
            {
                Debug.Log(itemEntry.Key + ": " + itemEntry.Value);
            }
        }
    }
}

