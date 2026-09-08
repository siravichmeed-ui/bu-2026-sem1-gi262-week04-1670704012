using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Solution
{
    public class ZombieParade : Character
    {
        // รายการส่วนของขบวน
        private LinkedList<GameObject> Parade = new LinkedList<GameObject>();

        [Header("Parade Settings")]
        public GameObject bodyPrefab;
        public float moveInterval = 0.5f;

        private Vector3 moveDirection;

        private InputAction growAction;

        private void Start()
        {
            // หา Action Grow
            growAction = InputSystem.actions.FindAction("Grow", false);

            if (growAction == null)
            {
                Debug.LogError(
                    "ZombieParade: หา Input Action 'Grow' ไม่เจอ! " +
                    "ให้ตรวจสอบ Input Actions ว่ามี Action ชื่อ Grow หรือไม่"
                );
            }

            // ตรวจสอบ MapGenerator
            if (mapGenerator == null)
            {
                Debug.LogError(
                    "ZombieParade: mapGenerator เป็น NULL! " +
                    "ตรวจสอบ Character หรือการ Assign MapGenerator"
                );
            }

            moveDirection = Vector3.up;
            isAlive = true;

            // เพิ่มหัวเข้า Parade
            if (!Parade.Contains(gameObject))
            {
                Parade.AddFirst(gameObject);
            }

            // เริ่มการเคลื่อนที่
            StartCoroutine(MoveParade());
        }

        private void Update()
        {
            // ป้องกัน NullReferenceException
            if (growAction != null && growAction.triggered)
            {
                Grow();
            }
        }

        private Vector3 RandomizeDirection()
        {
            List<Vector3> possibleDirections = new List<Vector3>
            {
                Vector3.up,
                Vector3.down,
                Vector3.left,
                Vector3.right
            };

            return possibleDirections[
                Random.Range(0, possibleDirections.Count)
            ];
        }

        // =========================================================
        // MOVE PARADE
        // =========================================================

        IEnumerator MoveParade()
        {
            while (isAlive)
            {
                // ไม่มีสมาชิกใน Parade
                if (Parade.Count == 0)
                {
                    Debug.LogError("ZombieParade: Parade ไม่มีสมาชิก");
                    yield break;
                }

                // ต้องมี MapGenerator
                if (mapGenerator == null)
                {
                    Debug.LogError(
                        "ZombieParade: mapGenerator เป็น NULL"
                    );

                    yield break;
                }

                // หัวขบวน
                LinkedListNode<GameObject> firstNode = Parade.First;

                // หางขบวน
                LinkedListNode<GameObject> lastNode = Parade.Last;

                if (firstNode == null || lastNode == null)
                {
                    Debug.LogError(
                        "ZombieParade: firstNode หรือ lastNode เป็น NULL"
                    );

                    yield break;
                }

                GameObject firstPart = firstNode.Value;
                GameObject lastPart = lastNode.Value;

                if (firstPart == null)
                {
                    Debug.LogError(
                        "ZombieParade: firstPart เป็น NULL"
                    );

                    yield break;
                }

                if (lastPart == null)
                {
                    Debug.LogError(
                        "ZombieParade: lastPart เป็น NULL"
                    );

                    yield break;
                }

                // =================================================
                // หาตำแหน่งใหม่ของหัว
                // =================================================

                int toX = 0;
                int toY = 0;

                bool isCollide = true;

                int safetyCounter = 0;

                while (isCollide)
                {
                    moveDirection = RandomizeDirection();

                    toX = Mathf.RoundToInt(
                        firstPart.transform.position.x + moveDirection.x
                    );

                    toY = Mathf.RoundToInt(
                        firstPart.transform.position.y + moveDirection.y
                    );

                    isCollide = IsCollision(toX, toY);

                    // ป้องกัน while loop ไม่จบ
                    safetyCounter++;

                    if (safetyCounter > 100)
                    {
                        Debug.LogWarning(
                            "ZombieParade: หาตำแหน่งเดินไม่ได้"
                        );

                        yield return new WaitForSeconds(moveInterval);
                        continue;
                    }
                }

                // =================================================
                // เอาหางออกจากตำแหน่งเดิม
                // =================================================

                Parade.RemoveLast();

                // =================================================
                // ล้างตำแหน่งเดิมใน Map
                // =================================================

                if (positionX >= 0 &&
                    positionY >= 0 &&
                    positionX < mapGenerator.mapdata.GetLength(0) &&
                    positionY < mapGenerator.mapdata.GetLength(1))
                {
                    mapGenerator.mapdata[positionX, positionY] = null;
                }

                // =================================================
                // เปลี่ยนตำแหน่ง
                // =================================================

                positionX = toX;
                positionY = toY;

                // =================================================
                // ย้ายส่วนหางมาเป็นหัว
                // =================================================

                lastPart.transform.position =
                    new Vector3(positionX, positionY, 0);

                // =================================================
                // ใส่กลับเข้า LinkedList ด้านหน้า
                // =================================================

                Parade.AddFirst(lastPart);

                // =================================================
                // อัปเดต Map
                // =================================================

                if (positionX >= 0 &&
                    positionY >= 0 &&
                    positionX < mapGenerator.mapdata.GetLength(0) &&
                    positionY < mapGenerator.mapdata.GetLength(1))
                {
                    mapGenerator.mapdata[positionX, positionY] = null;
                }

                // รอก่อนเดินครั้งต่อไป
                yield return new WaitForSeconds(moveInterval);
            }
        }

        // =========================================================
        // COLLISION
        // =========================================================

        private bool IsCollision(int x, int y)
        {
            if (mapGenerator == null)
            {
                return true;
            }

            return HasPlacement(x, y);
        }

        // =========================================================
        // MOVE
        // =========================================================

        void Move(Vector2 direction, GameObject targetMove)
        {
            int toX = Mathf.RoundToInt(direction.x);
            int toY = Mathf.RoundToInt(direction.y);

            Debug.Log(
                "Move to: " + toX + "," + toY
            );
        }

        // =========================================================
        // GROW
        // =========================================================

        private void Grow()
        {
            // ไม่มี Prefab
            if (bodyPrefab == null)
            {
                Debug.LogError(
                    "ZombieParade: bodyPrefab เป็น NULL! " +
                    "ให้ลาก Body Prefab ใส่ Inspector"
                );

                return;
            }

            // ไม่มีส่วนของ Parade
            if (Parade.Count == 0)
            {
                Debug.LogError(
                    "ZombieParade: Parade ไม่มีสมาชิก"
                );

                return;
            }

            // เอาส่วนสุดท้าย
            GameObject lastPart = Parade.Last.Value;

            if (lastPart == null)
            {
                Debug.LogError(
                    "ZombieParade: lastPart เป็น NULL"
                );

                return;
            }

            // สร้างส่วนใหม่
            GameObject newPart = Instantiate(bodyPrefab);

            // ให้อยู่ตำแหน่งเดียวกับหาง
            newPart.transform.position =
                lastPart.transform.position;

            // เพิ่มเข้าไปท้าย Parade
            Parade.AddLast(newPart);
        }
    }
}