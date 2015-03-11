using UnityEngine;
using System.Collections;

public class InpuManager : MonoBehaviour
{

    private static InpuManager instance;
    public static InpuManager Instance
    {
        get { return InpuManager.instance; }
    }

    public bool editorMode;

    void Awake()
    {
        if (InpuManager.instance == null)
        {
            InpuManager.instance = this;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
