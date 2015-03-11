using UnityEngine;
using System.Collections;

public class BlockSelectorManager : MonoBehaviour
{

    private static BlockSelectorManager instance;
    public static BlockSelectorManager Instance
    {
        get { return instance; }
    }
    private GameObject blockSelector;

    private Quaternion targetRotation;

    void Awake()
    {
        if (BlockSelectorManager.instance == null)
        {
            BlockSelectorManager.instance = this;
        }
    }

    void Start()
    {
        this.blockSelector = GameObject.Find("BlockSelector").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.targetRotation == null || this.targetRotation.Equals(Quaternion.identity))
        {
            return;
        }
        this.blockSelector.transform.rotation = Quaternion.Slerp(this.blockSelector.transform.rotation, this.targetRotation, Time.deltaTime);
        if (Mathf.Abs(this.targetRotation.x - this.blockSelector.transform.rotation.x) <= Quaternion.kEpsilon
            && Mathf.Abs(this.targetRotation.y - this.blockSelector.transform.rotation.y) <= Quaternion.kEpsilon
            && Mathf.Abs(this.targetRotation.z - this.blockSelector.transform.rotation.z) <= Quaternion.kEpsilon)
        {
            this.blockSelector.transform.rotation = this.targetRotation;
            this.targetRotation = Quaternion.identity;
        }
    }


    public void nextBlock()
    {
        this.rotateBlockSelector(30.0f);
    }

    public void previousBlock()
    {
        this.rotateBlockSelector(-30.0f);
    }

    private void rotateBlockSelector(float angle)
    {
        Debug.Log("Rotate block selector");
        this.targetRotation = (this.targetRotation.Equals(Quaternion.identity) ? this.blockSelector.transform.rotation : this.targetRotation) * Quaternion.AngleAxis(angle, this.transform.up);
        // this.blockSelector.transform.Rotate(this.transform.up, angle, Space.Self);
    }
}
