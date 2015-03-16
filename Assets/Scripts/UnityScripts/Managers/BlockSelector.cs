using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockSelector : MonoBehaviour
{
    public float dashRotationSpeed = 50.0f;
    public float rotationSpeed = 10.0f;
    private bool dash = false;
    private Quaternion targetRotation = Quaternion.identity;
    public Block.BlockType blockType = Block.BlockType.NONE;
    private List<Block> availablesBlocks = new List<Block>();
    private int currentBlockIndex = -1;
    private float rotationAngle = 0.0f;
    public int maxBlockDisplayedCount = 3;
    private Dictionary<int, BlockSelectorAnchor> anchors = new Dictionary<int, BlockSelectorAnchor>();
    //private int startAnchorIndex = 0;
    private int currentAnchorIndex = 0;
    private List<GameObject> displayedBlocks = new List<GameObject>();

    void Start()
    {
        if (this.blockType == Block.BlockType.NONE)
        {
            Debug.LogWarning("BlockType => NONE, useless block selector");
            this.enabled = false;
            return;
        }

        foreach (Block block in BlockSelectorManager.Instance.availablesBlocks)
        {
            if (block)
            {
                if (block.blockType == this.blockType)
                {
                    this.availablesBlocks.Add(block);
                }
            }
        }

        if (this.availablesBlocks.Count > 0)
        {
            this.currentBlockIndex = 0;
        }
        else
        {
            this.enabled = false;
            return;
        }

        foreach (BlockSelectorAnchor anchor in this.transform.GetComponentsInChildren<BlockSelectorAnchor>())
        {
            this.anchors.Add(anchor.id, anchor);
        }
        this.rotationAngle = 360.0f / this.anchors.Count;
        //this.currentAnchorIndex = this.startAnchorIndex;
        this.load();
    }

    private void load()
    {
        for (this.currentBlockIndex = 0; this.currentBlockIndex < (this.availablesBlocks.Count > this.maxBlockDisplayedCount ? this.maxBlockDisplayedCount : this.availablesBlocks.Count); this.currentBlockIndex++)
        {
            this.addDisplayedBlock(this.availablesBlocks[this.currentBlockIndex], this.clampAnchorIndex(this.currentAnchorIndex + this.currentBlockIndex), false);

        }
    }

    private int getFirstAnchorIndex()
    {
        int index = this.currentAnchorIndex - 1;
        return this.clampAnchorIndex(index);
    }

    private int getLastAnchorIndex()
    {
        int index = this.currentAnchorIndex + this.maxBlockDisplayedCount;
        return this.clampAnchorIndex(index);
    }

    private int clampAnchorIndex(int index)
    {
        if (index < 0)
        {
            return this.anchors.Count - 1;
        }
        else if (index >= this.anchors.Count)
        {
            return 0;
        }
        return index;
    }

    private int getFirstBlockIndex()
    {
        int index = this.currentBlockIndex - 1;
        return this.clampBlockIndex(index);
    }

    private int getLastBlockIndex()
    {
        int index = this.currentBlockIndex + this.maxBlockDisplayedCount;
        return this.clampBlockIndex(index);
    }

    private int clampBlockIndex(int index)
    {
        if (index < 0)
        {
            return this.availablesBlocks.Count - 1;
        }
        else if (index >= this.availablesBlocks.Count)
        {
            return 0;
        }
        return index;
    }

    private void addDisplayedBlock(Block block, int anchorIndex, bool head)
    {
        BlockSelectorAnchor anchor;
        if (this.anchors.TryGetValue(anchorIndex, out anchor))
        {
            GameObject go = Instantiate(block.gameObject, anchor.transform.position, Quaternion.identity) as GameObject;
            if (head)
            {
                this.displayedBlocks.Insert(0, go);
            }
            else
            {
                this.displayedBlocks.Add(go);
            }
            go.transform.parent = anchor.transform;
            ConfigurableJoint joint = go.GetComponent<ConfigurableJoint>();
            if (joint)
            {
                Rigidbody rb = anchor.GetComponent<Rigidbody>();
                if (rb)
                {
                    joint.connectedBody = rb;
                }
            }
        }
        else
        {
            Debug.LogError("No Anchor for index : " + anchorIndex);
        }
    }

    private void removeDisplayedBlock(bool head, int anchorIndex)
    {
        int indexToRemove = head ? 0 : this.displayedBlocks.Count - 1;
        GameObject block = this.displayedBlocks[indexToRemove];
        this.displayedBlocks.RemoveAt(indexToRemove);
        Destroy(block);
    }

    void Update()
    {
        if (this.dash)
        {
            if (InputManager.Instance.editorMode)
            {
                if (Input.GetMouseButton(0))
                {
                    rotateBlockSelector(Input.GetAxis("Mouse X") * this.dashRotationSpeed);
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    rotateBlockSelector(Input.GetTouch(0).deltaPosition.x * this.dashRotationSpeed);
                }
            }
        }
        else
        {
            if (this.targetRotation.Equals(Quaternion.identity))
            {
                return;
            }
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.targetRotation, Time.deltaTime * this.rotationSpeed);
            if (Mathf.Abs(this.targetRotation.x - this.transform.rotation.x) <= Quaternion.kEpsilon
                && Mathf.Abs(this.targetRotation.y - this.transform.rotation.y) <= Quaternion.kEpsilon
                && Mathf.Abs(this.targetRotation.z - this.transform.rotation.z) <= Quaternion.kEpsilon)
            {
                this.transform.rotation = this.targetRotation;
                this.targetRotation = Quaternion.identity;
            }
        }
    }

    private void clampCurrentBlockIndex()
    {
        if (this.currentBlockIndex < 0)
        {
            this.currentBlockIndex = this.availablesBlocks.Count - 1;
        }
        else if (this.currentBlockIndex >= this.availablesBlocks.Count)
        {
            this.currentBlockIndex = 0;
        }
    }

    public void nextBlock()
    {
        this.addDisplayedBlock(this.availablesBlocks[this.getFirstBlockIndex()], this.getFirstAnchorIndex(), true);
        this.removeDisplayedBlock(false, this.getLastAnchorIndex());
        this.currentBlockIndex--;
        this.clampCurrentBlockIndex();
        this.currentAnchorIndex--;
        this.currentAnchorIndex = this.clampAnchorIndex(this.currentAnchorIndex);
        this.rotateBlockSelector(-this.rotationAngle);
    }

    public void previousBlock()
    {
        this.addDisplayedBlock(this.availablesBlocks[this.getLastBlockIndex()], this.getLastAnchorIndex(), false);
        this.removeDisplayedBlock(true, this.getFirstAnchorIndex());
        this.currentBlockIndex++;
        this.clampCurrentBlockIndex();
        this.currentAnchorIndex++;
        this.currentAnchorIndex = this.clampAnchorIndex(this.currentAnchorIndex);
        this.rotateBlockSelector(this.rotationAngle);
    }

    private void rotateBlockSelector(float angle)
    {
        this.targetRotation = (this.targetRotation.Equals(Quaternion.identity) ? this.transform.rotation : this.targetRotation) * Quaternion.AngleAxis(angle, this.transform.up);
    }

    void OnMouseDown()
    {
        this.dash = true;
    }

    void OnMouseUp()
    {
        this.dash = false;
    }
}
