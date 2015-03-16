using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public float sentivity_X = 2.0f;
    //public float sentivity_Y = 2.0f;
    private BlockPicker blockPicker;
    //public bool invertVerticalAxis;
    private float touchTime = Mathf.Infinity;
    public float doubleTapMaxDelay = 0.3f;
    Animator playerAnim;
    private bool canShow;
    public float durationBetweenShow = 2.0f;
    private bool canRotate;
    private RaycastHit hit;

    void Start()
    {
        this.canShow = true;
        this.canRotate = false;
        this.blockPicker = Camera.main.GetComponent<BlockPicker>();
        this.playerAnim = this.transform.FindChild("Robot").FindChild("Arakne").GetComponentInChildren<Animator>();
    }

    void Update()
    {
        /* if (InputManager.Instance.editorMode)
         {
             if (Input.GetMouseButtonDown(0))
             {
                 if (Mathf.Abs(Time.timeSinceLevelLoad - this.touchTime) <= this.doubleTapMaxDelay)
                 {
                     this.touchTime = Mathf.Infinity;
                     Debug.Log("Double Tap");
                     this.playAnim();
                 }
                 else
                 {
                     this.touchTime = Time.timeSinceLevelLoad;
                 }
             }
             if (Input.GetMouseButtonUp(0))
             {
                 this.canRotate = false;
             }
         }
         else
         {
             for (int i = 0; i < Input.touchCount; i++)
             {
                 if (Input.touches[i].tapCount >= 2)
                 {
                     Debug.Log("Double Tap");
                     this.playAnim();
                     break;
                 }
             }
         }*/

        if (this.blockPicker.hasPickableSelected() || !this.canRotate)
        {
            return;
        }
        if (InputManager.Instance.editorMode)
        {
            this.transform.Rotate(this.transform.up, Input.GetAxis("Mouse X") * -1 * this.sentivity_X * Time.deltaTime);
        }
        else
        {
            this.transform.Rotate(this.transform.up, Input.GetTouch(0).deltaPosition.x * -1 * this.sentivity_X * Time.deltaTime);
        }
    }

    private void playAnim()
    {
        if (this.playerAnim && canShow)
        {
            this.playerAnim.SetTrigger("makeShow");
            StartCoroutine(this.waitBetweenShow());
        }
    }

    private IEnumerator waitBetweenShow()
    {
        this.canShow = false;
        yield return new WaitForSeconds(this.durationBetweenShow);
        this.canShow = true;
    }

    void OnMouseDown()
    {
        this.canRotate = true;
        if (Mathf.Abs(Time.timeSinceLevelLoad - this.touchTime) <= this.doubleTapMaxDelay)
        {
            this.touchTime = Mathf.Infinity;
            Debug.Log("Double Tap");
            this.playAnim();
        }
        else
        {
            this.touchTime = Time.timeSinceLevelLoad;
        }
    }

    void OnMouseUp()
    {
        this.canRotate = false;
    }
}
