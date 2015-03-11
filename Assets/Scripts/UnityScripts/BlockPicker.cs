using UnityEngine;
using System.Collections;

public class BlockPicker : MonoBehaviour
{
    public float followSpeed = 2.0f;
    public float dockSpeed = 1.0f;
    private GameObject picked;
    private RaycastHit hit;
    private float dist;
    private string anchorLayerName = "Anchor";
    private string pickableLayerName = "Block";
    public float floatRange = 5.0f;
    private bool docked;
    public GameObject blockDestroyEffect;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out this.hit, Mathf.Infinity, LayerMask.GetMask(this.pickableLayerName)))
            {
                Debug.Log("Picked");
                this.picked = this.hit.collider.gameObject;
                this.docked = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (this.picked != null)
            {
                Debug.Log("Released picked");
                if (!this.docked)
                {
                    if (this.blockDestroyEffect)
                    {
                        GameObject destroyEffect = Instantiate(this.blockDestroyEffect, this.picked.transform.position, Quaternion.identity) as GameObject;
                        Destroy(destroyEffect, 2.0f);
                    }
                    Destroy(this.picked);
                }
                this.picked = null;
            }
        }

        if (this.picked != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out this.hit, Mathf.Infinity, LayerMask.GetMask(this.anchorLayerName)))
            {
                this.picked.transform.position = hit.collider.transform.position;// Vector3.Lerp(this.picked.transform.position, hit.collider.transform.position, Time.deltaTime * this.dockSpeed);
                this.picked.transform.rotation = hit.collider.transform.rotation;
                this.picked.transform.parent = hit.collider.transform.parent;
                this.docked = true;
            }
            else
            {
                Debug.Log("Move picked");
                this.picked.transform.position = Vector3.Lerp(this.picked.transform.position, ray.GetPoint(this.floatRange), Time.deltaTime * this.followSpeed);
                this.docked = false;
            }
        }
    }

    public bool hasPickableSelected()
    {
        return this.picked != null;
    }

    public void addBlock()
    {
        //create block
        //temp
        string id = "02";
        //temp
        this.picked = GameObject.Instantiate(Resources.Load("Prefabs/Block" + id)) as GameObject;
        this.picked.transform.position = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(this.floatRange);
    }
}
