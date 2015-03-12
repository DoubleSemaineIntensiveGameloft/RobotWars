using UnityEngine;
using System.Collections;

public class CategorySelector : MonoBehaviour
{
    public Block.BlockType blockType = Block.BlockType.NONE;
    public Color selectedColor = Color.green;//TODO: button color selection

    void Start()
    {

    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        BlockSelectorManager.Instance.setCurrentBlockSelector(this.blockType);
    }
}
