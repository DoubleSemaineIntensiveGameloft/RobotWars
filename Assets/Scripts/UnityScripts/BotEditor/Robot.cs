using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Robot : MonoBehaviour
{
    [System.Serializable]
    public struct Skin
    {
        public string id;
        public Sprite icon;
        public GameObject model;
    }
    public int maxActiveCount = 3;
    public int maxPassiveCount = 2;
    private Dictionary<Block.BlockType, List<Block>> blocks = new Dictionary<Block.BlockType, List<Block>>();
    public Skin[] availablesSkins;

    public bool canUseBlock(Block.BlockType blockType)
    {
        switch (blockType)
        {
            case Block.BlockType.ACTIVE:
                if (this.getActiveBlockCount() < this.maxActiveCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Block.BlockType.PASSIVE:
                if (this.getPassiveBlockCount() < this.maxPassiveCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                Debug.LogError("Error => Unknown block type");
                return false;
        }
    }

    public bool addBlock(Block block)
    {
        if (this.canUseBlock(block.blockType))
        {
            this.getOwnersCreate(block.blockType).Add(block);
            return true;
        }
        else
        {
            GUIManager.Instance.displayMessage("Max " + block.blockType + " blocks reached");
            return false;
        }
    }

    public void removeBlock(Block block)
    {
        this.blocks[block.blockType].Remove(block);
    }

    private List<Block> getOwnersCreate(Block.BlockType blockType)
    {
        if (this.blocks.ContainsKey(blockType))
        {
            return this.blocks[blockType];
        }
        else
        {
            List<Block> blockList = new List<Block>();
            this.blocks.Add(blockType, blockList);
            return blockList;
        }
    }

    public int getBlockCount(Block.BlockType blockType)
    {
        return this.getOwnersCreate(blockType).Count;
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

    public Sprite getIcon(string materialId)
    {
        foreach (Skin skin in this.availablesSkins)
        {
            if (skin.id.Equals(materialId))
            {
                return skin.icon;
            }
        }
        return null;
    }

    public void applySkin(string skinId)
    {
        foreach (Skin skin in this.availablesSkins)
        {
            if (skin.id.Equals(skinId))
            {
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in transform)
                {
                    children.Add(child.gameObject);
                }
                children.ForEach(child => Destroy(child));

                GameObject newBot = Instantiate(skin.model, Vector3.zero, skin.model.transform.rotation) as GameObject;
                newBot.transform.parent = this.transform;
                newBot.transform.localPosition = Vector3.zero;
                newBot.transform.localRotation = Quaternion.identity;
                newBot.transform.localScale = Vector3.one;
                break;
            }
        }
    }
}
