using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{

    private static InputManager instance;
    public static InputManager Instance
    {
        get { return InputManager.instance; }
    }

    public bool editorMode;

    void Awake()
    {
        if (InputManager.instance == null)
        {
            InputManager.instance = this;
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
