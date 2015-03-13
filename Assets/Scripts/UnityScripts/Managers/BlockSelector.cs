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
    private float rotationAngle = 30.0f;
    private List<Block> displayedBlock = new List<Block>();
    public int maxBlockDisplayedCount = 3;

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
            if (block.blockType == this.blockType)
            {
                this.availablesBlocks.Add(block);
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
        this.load();
    }

    private void load()
    {
        for (int i = this.currentBlockIndex; i < (this.availablesBlocks.Count > this.maxBlockDisplayedCount ? this.maxBlockDisplayedCount : this.availablesBlocks.Count); i++)
        {
            this.addDisplayedBlock(this.availablesBlocks[i]);
        }
    }

    private void addDisplayedBlock(Block block)
    {
        this.displayedBlock.Add(block);
        GameObject go = Instantiate(block.gameObject);
        go.transform.parent = this.transform;
        float radius = 3.3f;
        Vector3 pos = new Vector3(Mathf.Cos(Random.Range(-1.0f, 1.0f)), 0.0f, Mathf.Sin(Random.Range(-1.0f, 1.0f)));
        pos.Normalize();
        go.transform.localPosition = pos * radius;
    }

    private void removeDisplayedBlock(Block block)
    {

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

    private void checkCurrentBlockIndex()
    {
        if (this.currentBlockIndex < 0)
        {
            this.currentBlockIndex = this.availablesBlocks.Count - 1;
        }
        else if (this.currentBlockIndex == this.availablesBlocks.Count)
        {
            this.currentBlockIndex = 0;
        }
    }

    public void nextBlock()
    {
        this.currentBlockIndex++;
        this.checkCurrentBlockIndex();
        this.rotateBlockSelector(this.rotationAngle);
    }

    public void previousBlock()
    {
        this.currentBlockIndex--;
        this.checkCurrentBlockIndex();
        this.rotateBlockSelector(-this.rotationAngle);
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
