using System;
using System.Collections.Generic;
using System.Data;

namespace CP380_B1_BlockList.Models
{
    public class BlockList
    {
        public IList<Block> Chain { get; set; }

        public int Difficulty { get; set; } = 2;

        public BlockList()
        {
            Chain = new List<Block>();
            MakeFirstBlock();
        }

        public void MakeFirstBlock()
        {
            var block = new Block(DateTime.Now, null, new List<Payload>());
            block.Mine(Difficulty);
            Chain.Add(block);
        }

        public void AddBlock(Block block)
        {
            // TODO - Done
            var newBlock = new Block(block.TimeStamp, block.PreviousHash, block.Data);
            newBlock.Mine(Difficulty);
            Chain.Add(newBlock);            
        }

        public bool IsValid()
        {
            // TODO
            for (int i = 1; i < Chain.Count; i++)
            {
                Block prev = Chain[i - 1];
                Block curt = Chain[i];
                if (curt.Hash != curt.CalculateHash())
                    return false;
                if (curt.PreviousHash != prev.Hash)
                    return false;
            }
            return true;
        }
    }
}
