using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Robot : MonoBehaviour
{
    public int maxActiveCount = 3;
    public int maxPassiveCount = 2;
    private Dictionary<Block.BlockType, List<Block>> blocks = new Dictionary<Block.BlockType, List<Block>>();


    public bool addBlock(Block block)
    {
        switch (block.blockType)
        {
            case Block.BlockType.ACTIVE:
                if (this.getActiveBlockCount() < this.maxActiveCount)
                {
                    this.getOwnersCreate(block.blockType).Add(block);
                    return true;
                }
                else
                {
                    GUIManager.Instance.displayMessage("Max active blocks reached");
                    return false;
                }
            case Block.BlockType.PASSIVE:
                if (this.getPassiveBlockCount() < this.maxPassiveCount)
                {
                    this.getOwnersCreate(block.blockType).Add(block);
                    return true;
                }
                else
                {
                    GUIManager.Instance.displayMessage("Max passive blocks reached");
                    return false;
                }
            case Block.BlockType.NONE:
            default:
                Debug.LogError("Error => Unknown block type");
                return false;
        }
    }

    public void removeBlock(Block block)
    {
        this.blocks[block.blockType].Remove(block);
    }

    private List<Block> getOwnersCreate(Block.BlockType blockType)
    {
        List<Block> blockList = this.blocks[blockType];
        if (blockList == null)
        {
            blockList = new List<Block>();
            this.blocks[blockType] = blockList;
        }
        return blockList;
    }

    public int getBlockCount(Block.BlockType blockType)
    {
        return this.blocks[blockType].Count;
    }

    public int getActiveBlockCount()
    {
        return this.getBlockCount(Block.BlockType.ACTIVE);
    }

    public int getPassiveBlockCount()
    {
        return this.getBlockCount(Block.BlockType.PASSIVE);
    }

    public string export()
    {
        return "";
    }
}
