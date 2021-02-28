using System.Collections.Generic;
using UnityEngine;

namespace Wowie
{
    public class PlayerTail: MonoBehaviour
    {
        public List<TailBlock> Tail = new List<TailBlock>();
        public float Spacing;

        public void AddBlock(TailBlock newBlock, BlockPickup pickup)
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

        public void BreakAt(TailBlock block)
        {
            BreakAt(Tail.FindIndex(it => it == block));
        }

        public void BreakAt(int idx)
        {
            if (idx > -1 && idx < Tail.Count)
            {
                for (int i = idx; i < Tail.Count; i++)
                {
                    Tail[i].OnBreakOff();
                }
                Tail.RemoveRange(idx, Tail.Count - idx);
            }
        }
    }
}