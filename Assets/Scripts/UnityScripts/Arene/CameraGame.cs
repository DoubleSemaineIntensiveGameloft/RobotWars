using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraGame : MonoBehaviour {

    [Range(-1,1)]
    public float cameraGyro = 0;

    [Range(0, 1)]
    public float cameraZoom = 0;

    public float zoomFactor = 10;

    public float actCameraGyro = 0;


    public Vector3 topPosition;
    public Vector3 sidePosition;

    public bool debugPC = false;

	void Start () {

	}
	
	void Update () {
        

        if(debugPC)
        { 
            cameraGyro = Mathf.Round(Input.acceleration.y * -1);
        }


        actCameraGyro = Mathf.Lerp(actCameraGyro, cameraGyro, Time.deltaTime * 2);


        transform.position = Vector3.Lerp(topPosition, sidePosition, actCameraGyro);

        
        Vector3 meanPointBots = (ManagerGame.instance.bots[0].transform.position + ManagerGame.instance.bots[1].transform.position) / 2;
        transform.position = new Vector3(meanPointBots.x, transform.position.y, meanPointBots.z);

        float distanceBots = Vector3.Distance(ManagerGame.instance.bots[0].transform.position, ManagerGame.instance.bots[1].transform.position);


        transform.position += transform.forward * (zoomFactor - distanceBots / zoomFactor * 5);

        //transform.LookAt(Vector3.zero);
	}

    public bool gyoBool { get; set; }

    public Gyroscope gyro { get; set; }
}
