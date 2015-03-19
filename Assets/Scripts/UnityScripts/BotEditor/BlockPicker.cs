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
    private Transform player;
    private float forcedDockedScale = 1.0f;
    private GameObject dockedAnchor = null;
    public float pickingDelay = 0.8f;
    //private IEnumerator pickingDelayFunc = null;
    private GameObject hitBlock = null;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        //this.pickingDelayFunc = this.pickingTimer();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out this.hit, Mathf.Infinity, LayerMask.GetMask(this.pickableLayerName)))
            {
                //Debug.Log("Picked");
                if (hit.collider.transform.parent != null && hit.collider.transform.parent.tag.Equals("Anchor"))
                {
                    StopCoroutine("pickingTimer");
                    this.hitBlock = hit.collider.gameObject;
                    StartCoroutine("pickingTimer");
                    //MeshRenderer r = hit.collider.transform.parent.GetComponentInChildren<MeshRenderer>();
                    //if (r)
                    //{
                    //    r.enabled = true;
                    //}
                    //this.picked = hit.collider.gameObject;
                    //Block block = this.picked.GetComponent<Block>();
                    //RobotsManager.Instance.getCurrentRobot().removeBlock(block);
                    //Collider collider = this.picked.GetComponent<Collider>();
                    //if (collider)
                    //{
                    //    collider.enabled = false;
                    //    //Debug.Log("DisableCollider");
                    //}
                }
                else
                {
                    Block block = hit.collider.gameObject.GetComponent<Block>();
                    if (block)
                    {
                        //Debug.Log("Block description : " + block.description);
                        if (RobotsManager.Instance.getCurrentRobot().canUseBlock(block.blockType))
                        {
                            GUIManager.Instance.setBlockDescription(block.description);
                            this.picked = Instantiate(hit.collider.gameObject) as GameObject;
                            ConfigurableJoint joint = this.picked.GetComponent<ConfigurableJoint>();
                            if (joint)
                            {
                                joint.connectedBody = null;
                                Destroy(joint);
                            }
                            Rigidbody rb = this.picked.GetComponent<Rigidbody>();
                            if (rb)
                            {
                                rb.useGravity = false;
                                rb.velocity = Vector3.zero;
                                rb.angularVelocity = Vector3.zero;
                            }
                            this.picked.transform.position = hit.collider.gameObject.transform.position;
                            this.picked.transform.rotation = hit.collider.gameObject.transform.rotation;
                            Collider collider = this.picked.GetComponent<Collider>();
                            if (collider)
                            {
                                collider.enabled = false;
                                //Debug.Log("DisableCollider");
                            }
                        }
                        else
                        {
                            GUIManager.Instance.displayMessage("Max " + block.blockType + " blocks reached");
                        }
                        this.docked = false;
                    }
                    else
                    {
                        Debug.LogError("No block");
                    }

                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine("pickingTimer");
            this.hitBlock = null;
            if (this.picked != null)
            {
                //Debug.Log("Released picked");
                if (docked)
                {
                    Block block = this.dockedAnchor.GetComponentInChildren<Block>();
                    MeshRenderer r = this.dockedAnchor.GetComponentInChildren<MeshRenderer>();
                    if (r)
                    {
                        r.enabled = false;
                    }
                    if (block)
                    {
                        RobotsManager.Instance.getCurrentRobot().removeBlock(block);
                        this.spawnDestructionEffect(block.gameObject.transform.position);
                        Destroy(block.gameObject);
                    }
                    Collider collider = this.picked.GetComponent<Collider>();
                    if (collider)
                    {
                        collider.enabled = true;
                        //Debug.Log("DisableCollider");
                    }
                    this.picked.transform.parent = this.dockedAnchor.transform;
                    RobotsManager.Instance.getCurrentRobot().addBlock(this.picked.GetComponent<Block>());
                }
                else
                {
                    this.spawnDestructionEffect(this.picked.transform.position);
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
                //this.picked.transform.parent = hit.collider.transform;
                this.docked = true;
                this.dockedAnchor = hit.collider.gameObject;
                this.picked.transform.localScale = new Vector3(this.forcedDockedScale, this.forcedDockedScale, this.forcedDockedScale);
            }
            else
            {
                //Debug.Log("Move picked");
                this.dockedAnchor = null;
                this.picked.transform.parent = null;
                this.picked.transform.position = Vector3.Lerp(this.picked.transform.position, ray.GetPoint(this.floatRange), Time.deltaTime * this.followSpeed);
                this.docked = false;
                this.picked.transform.localScale = Vector3.Lerp(this.picked.transform.localScale, new Vector3(this.forcedDockedScale, this.forcedDockedScale, this.forcedDockedScale), Time.deltaTime * this.followSpeed);
            }
        }
    }

    private void spawnDestructionEffect(Vector3 position)
    {
        if (this.blockDestroyEffect)
        {
            GameObject destroyEffect = Instantiate(this.blockDestroyEffect, position, Quaternion.identity) as GameObject;
            Destroy(destroyEffect, 2.0f);
        }
    }

    public bool hasPickableSelected()
    {
        return this.picked != null;
    }

    private IEnumerator pickingTimer()
    {
        yield return new WaitForSeconds(this.pickingDelay);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out this.hit, Mathf.Infinity, LayerMask.GetMask(this.pickableLayerName)))
        {
            if (this.hitBlock.Equals(hit.collider.gameObject))
            {
                MeshRenderer r = hit.collider.transform.parent.GetComponentInChildren<MeshRenderer>();
                if (r)
                {
                    r.enabled = true;
                }
                this.picked = hit.collider.gameObject;
                Block block = this.picked.GetComponent<Block>();
                RobotsManager.Instance.getCurrentRobot().removeBlock(block);
                Collider collider = this.picked.GetComponent<Collider>();
                if (collider)
                {
                    collider.enabled = false;
                    //Debug.Log("DisableCollider");
                }
            }
        }
    }

    //public void addBlock()
    //{
    //    //create block
    //    //temp
    //    string id = "02";
    //    //temp
    //    this.picked = GameObject.Instantiate(Resources.Load("Prefabs/Block" + id)) as GameObject;
    //    this.picked.transform.position = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(this.floatRange);
    //}

    private void checkPosition()
    {
        if (this.picked == null)
        {
            return;
        }
        Vector3 relativePos = this.player.InverseTransformPoint(this.picked.transform.position);
        //X
        if (relativePos.x < 0.0f)
        {
            //left
        }
        else if (relativePos.x > 0.0f)
        {
            //right
        }
        else
        {
            //axis
        }

        //Y
        if (relativePos.y < 0.0f)
        {
            //bottom
        }
        else if (relativePos.y > 0.0f)
        {
            //top
        }
        else
        {
            //axis
        }

        //Z
        if (relativePos.z < 0.0f)
        {
            //front
        }
        else if (relativePos.z > 0.0f)
        {
            //back
        }
        else
        {
            //axis
        }
    }
}
