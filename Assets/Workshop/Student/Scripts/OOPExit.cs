using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Solution
{

    public class OOPExit : Identity
    {
        public GameObject YouWin;
        public string ItemToOpen = "Key";
        public int ItemAmontToOpen = 2;
        // กำหนดชื่อไอเท็มและจำนวนที่ต้องการใช้ในการเปิดทางออก

        public override bool Hit()
        {
            // ตรวจสอบว่าผู้เล่นมีไอเท็มที่ต้องการหรือไม่
            bool isHasItemAmount = mapGenerator.player.inventory.HasItem(ItemToOpen, ItemAmontToOpen);
            if (isHasItemAmount)
            {
                YouWin.SetActive(true);
                Debug.Log("You win");
                return true;
            }
            else
            {
                Debug.Log("Need" + ItemToOpen + " " + ItemAmontToOpen + " to open");
            }

            return true;
          
        }
    }
}