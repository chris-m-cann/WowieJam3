using System.Collections.Generic;
using UnityEngine;
using Util.Events;

namespace Wowie
{
    public class PlayerTail: MonoBehaviour
    {
        public List<TailBlock> Tail = new List<TailBlock>();
        public float Spacing;

        [SerializeField] private VoidGameEvent onBreak;


        public void AddBlock(TailBlock newBlock, BlockPickup pickup)
        {
            if (Tail.Count < 2)
            {
                var end = transform;
                if (Tail.Count > 0)
                {
                    end = Tail[Tail.Count - 1].transform;
                }

                var pos = end.position - (Spacing * end.right.normalized);
                var block = Instantiate(newBlock, pos, Quaternion.identity);
                block.Pickup = pickup;
                pickup.OnPickup();
                Tail.Add(block);
            }
            else
            {

                var newPos = Tail[1].transform.position;
                var newRot = Tail[1].transform.rotation;

                var endMinus1Pos = Tail[Tail.Count - 2].transform.position;

                for (int i = Tail.Count - 2; i > 0; i--)
                {
                    Tail[i].transform.position = Tail[i + 1].transform.position;
                    Tail[i].transform.rotation = Tail[i + 1].transform.rotation;
                }

                var end = Tail[Tail.Count - 1].transform;
                var pos = end.position - (Spacing * end.right.normalized);
                end.position = pos;


                var block = Instantiate(newBlock, newPos, newRot);
                block.Pickup = pickup;
                pickup.OnPickup();
                Tail.Insert(1, block);
            }


        }

        public void BreakAt(TailBlock block)
        {
            BreakAt(Tail.FindIndex(it => it == block));
        }

        public void BreakAt(int idx)
        {
            if (idx > -1 && idx < Tail.Count)
            {
                idx = Mathf.Max(1, idx);
                for (int i = idx; i < Tail.Count; i++)
                {
                    Tail[i].OnBreakOff();
                }
                Tail.RemoveRange(idx, Tail.Count - idx);
                onBreak.Raise();
            }
        }
    }
}