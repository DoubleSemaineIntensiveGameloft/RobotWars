using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{

    public float actTimeAlive = 0;
    public float timeAlive = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        actTimeAlive += Time.deltaTime;

        if (actTimeAlive > timeAlive)
        {
            Destroy(gameObject);
        }

    }
}
