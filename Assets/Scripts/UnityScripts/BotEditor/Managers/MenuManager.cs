using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

    private static MenuManager instance;
    public static MenuManager Instance
    {
        get { return MenuManager.instance; }
    }

    void Awake()
    {
        if (MenuManager.instance == null)
        {
            MenuManager.instance = this;
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }


}
