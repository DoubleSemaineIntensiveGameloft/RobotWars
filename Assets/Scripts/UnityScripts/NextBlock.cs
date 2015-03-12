using UnityEngine;
using System.Collections;

public class NextBlock : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        BlockSelectorManager.Instance.getCurrentBlockSelector().nextBlock();
    }
}
