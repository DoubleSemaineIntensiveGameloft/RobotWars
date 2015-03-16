using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockSelectorManager : MonoBehaviour
{
    private static BlockSelectorManager instance;
    public static BlockSelectorManager Instance
    {
        get { return BlockSelectorManager.instance; }
    }

    private Dictionary<Block.BlockType, BlockSelector> selectorManagers;
    private BlockSelector currentBlockSelector;
    public Block.BlockType defaultBlockType = Block.BlockType.ACTIVE;
    public List<Block> availablesBlocks;

    void Awake()
    {
        if (BlockSelectorManager.instance == null)
        {
            BlockSelectorManager.instance = this;
        }
    }

    void Start()
    {
        this.selectorManagers = new Dictionary<Block.BlockType, BlockSelector>();
        BlockSelector[] selectors = GameObject.FindObjectsOfType<BlockSelector>();
        foreach (BlockSelector selector in selectors)
        {
            this.selectorManagers.Add(selector.blockType, selector);
        }
        if (this.selectorManagers.Count <= 0)
        {
            Debug.LogWarning("No blockSelector found !");
            this.enabled = false;
            return;
        }
        this.setCurrentBlockSelector(this.defaultBlockType);
    }

    void Update()
    {

    }

    public void setCurrentBlockSelector(Block.BlockType blockType)
    {
        BlockSelector selector = this.selectorManagers[blockType];
        if (selector == null)
        {
            Debug.LogError("No selector for block type : " + blockType);
            return;
        }
        this.setCurrentBlockSelectorActivated(false);
        this.currentBlockSelector = selector;
        this.setCurrentBlockSelectorActivated(true);
    }

    private void setCurrentBlockSelectorActivated(bool activated)
    {
        if (this.currentBlockSelector)
        {
            Animator animator = this.currentBlockSelector.GetComponent<Animator>();
            if (animator)
            {
                animator.SetBool("selected", activated);
            }
        }
    }

    public BlockSelector getCurrentBlockSelector()
    {
        return this.currentBlockSelector;
    }
}
