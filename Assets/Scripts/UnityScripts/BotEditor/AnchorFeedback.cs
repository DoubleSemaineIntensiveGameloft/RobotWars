using UnityEngine;
using System.Collections;

public class AnchorFeedback : MonoBehaviour
{
    public Vector3 endScale = Vector3.one;
    private Vector3 startScale;
    private Vector3 targetScale;
    public float speed = 20.0f;

    void Start()
    {
        this.startScale = this.transform.localScale;
        this.targetScale = this.endScale;
    }

    void Update()
    {
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.targetScale, this.speed * Time.deltaTime);
        if (this.transform.localScale == this.targetScale)
        {
            this.switchTarget();
        }
    }

    private void switchTarget()
    {
        if (this.targetScale == this.startScale)
        {
            this.targetScale = this.endScale;
        }
        else
        {
            this.targetScale = this.startScale;
        }
    }
}
