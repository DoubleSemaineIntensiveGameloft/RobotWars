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

    public int cameraState = 0; // 0 = cinématique, 1 = jeu
    public int cineState = 0;

    public CinematiquePoint[] cinePoints;

    public float actTimeCine = 0;
    public float timeCine = 2;

    public Animator[] panels;


	void Start () {


        ManagerGame.instance.limiter.SetActive(false);

        ManagerGame.instance.player1.GetComponentInChildren<Animator>().SetTrigger("makeShow");
	}
	
	void Update () {
        
        if(cameraState == 1)
        { 

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
        }
        else if(cameraState == 0)
        {
            actTimeCine += Time.deltaTime;
            transform.position = Vector3.Lerp(cinePoints[cineState].start.transform.position, cinePoints[cineState].end.transform.position, actTimeCine / timeCine);
            transform.LookAt(cinePoints[cineState].lookAt.transform.position);
            if(actTimeCine > timeCine)
            {
                cineState++;

                if (cineState == 1)
                {
                    ManagerGame.instance.player2.GetComponentInChildren<Animator>().SetTrigger("makeShow");
                }
                actTimeCine = 0;
                if(cineState >= cinePoints.Length)
                {
                    cameraState++;
                    foreach (Animator anim in panels)
                    {
                        anim.SetTrigger("AnimIn");
                    }

                    ManagerGame.instance.limiter.SetActive(true);

                }
            }
        }

        //transform.LookAt(Vector3.zero);
	}

    public void EndCine()
    {
        cameraState = 1;
    }

    public bool gyoBool { get; set; }

    public Gyroscope gyro { get; set; }
}
