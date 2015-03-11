using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public float sentivity_X = 2.0f;
    //public float sentivity_Y = 2.0f;
    private BlockPicker blockPicker;
    //public bool invertVerticalAxis;

    void Start()
    {
        this.blockPicker = Camera.main.GetComponent<BlockPicker>();
    }

    void Update()
    {
        if (this.blockPicker.hasPickableSelected())
        {
            return;
        }
        if (InpuManager.Instance.editorMode)
        {
            if (Input.GetMouseButton(0))
            {
                this.transform.Rotate(this.transform.up, Input.GetAxis("Mouse X") * -1 * this.sentivity_X);
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                this.transform.Rotate(this.transform.up, Input.GetTouch(0).deltaPosition.x * -1 * this.sentivity_X);
            }
        }
    }
}
