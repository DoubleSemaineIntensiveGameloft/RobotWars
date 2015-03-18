using UnityEngine;
using System.Collections;

public class CategorySelector : MonoBehaviour
{
    public Block.BlockType blockType;
    private Color selectedColor = Color.green;//TODO: button color selection
    private Color initColor;

    void Start()
    {
        //this.initColor = this.GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        BlockSelectorManager.Instance.setCurrentBlockSelector(this.blockType);
        //this.GetComponent<MeshRenderer>().material.color = selectedColor;
    }
}
