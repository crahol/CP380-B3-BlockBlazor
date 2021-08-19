using CP380_B1_BlockList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B3_BlockBlazor.Data
{
    public class MiningService
    {
        BlockService _blockServiceObj = new BlockService();
        PendingTransactionService _pendingTransactionServiceObj = new PendingTransactionService();
        public MiningService(BlockService blockService, PendingTransactionService pendingTransactionService)
        {
            _blockServiceObj = blockService;
            _pendingTransactionServiceObj = pendingTransactionService;
        }
        public async Task<Block> MineBlock(IEnumerable<Block> blockList)
        {
            Block lastBlock = blockList.Last();
            Block block = new Block(DateTime.Now, lastBlock.Hash, _pendingTransactionServiceObj.payloads);
            block.Mine(2);

            return block;
        }
    }
}
