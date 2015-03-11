using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraGame : MonoBehaviour {

    [Range(-1,1)]
    public float cameraGyro = 0;

    public float actCameraGyro = 0;


    public Vector3 topPosition;
    public Vector3 sidePosition;

    public bool debugPC = false;

	void Start () {

	}
	
	void Update () {
        

        if(!debugPC)
        { 
            cameraGyro = Mathf.Round(Input.acceleration.y * -1);
        }


        actCameraGyro = Mathf.Lerp(actCameraGyro, cameraGyro, Time.deltaTime * 2);

        transform.position = Vector3.Lerp(topPosition, sidePosition, actCameraGyro);



        transform.LookAt(Vector3.zero);
	}

    public bool gyoBool { get; set; }

    public Gyroscope gyro { get; set; }
}
