using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class VehicleCamera : MonoBehaviour
{



    public Transform target;

    public float smooth = 0.3f;
    public float distance = 5.0f;
    public float height = 1.0f;
    public float Angle = 20;


    public List<Transform> cameraSwitchView;
    public LayerMask lineOfSightMask = 0;

    public CarUIClass CarUI;



    private float yVelocity = 0.0f;
    private float xVelocity = 0.0f;
    [HideInInspector]
    public int Switch;

    private int gearst = 0;
    private float thisAngle = -150;
    private float restTime = 0.0f;


    private Rigidbody myRigidbody;



    private VehicleControl carScript;



    [System.Serializable]
    public class CarUIClass
    {

        public Image tachometerNeedle;
        public Image barShiftGUI;

        public Text speedText;
        public Text GearText;

    }





    ////////////////////////////////////////////// TouchMode (Control) ////////////////////////////////////////////////////////////////////


    private int PLValue = 0;


    public void PoliceLightSwitch()
    {

        if (!target.gameObject.GetComponent<PoliceLights>()) return;

        PLValue++;

        if (PLValue > 1) PLValue = 0;

        if (PLValue == 1)
            target.gameObject.GetComponent<PoliceLights>().activeLight = true;

        if (PLValue == 0)
            target.gameObject.GetComponent<PoliceLights>().activeLight = false;


    }


    public void CameraSwitch()
    {
        Switch++;
        if (Switch > cameraSwitchView.Count) { Switch = 0; }
    }


    public void CarAccelForward(float amount)
    {
        carScript.accelFwd = amount;
    }

    public void CarAccelBack(float amount)
    {
        carScript.accelBack = amount;
    }

    public void CarSteer(float amount)
    {
        carScript.steerAmount = amount;
    }

    public void CarHandBrake(bool HBrakeing)
    {
        carScript.brake = HBrakeing;
    }

    public void CarShift(bool Shifting)
    {
        carScript.shift = Shifting;
    }



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    public void RestCar()
    {

        if (restTime == 0)
        {
            myRigidbody.AddForce(Vector3.up * 500000);
            myRigidbody.MoveRotation(Quaternion.Euler(0, transform.eulerAngles.y, 0));
            restTime = 2.0f;
        }

    }




    public void ShowCarUI()
    {



        gearst = carScript.currentGear;
        CarUI.speedText.text = ((int)carScript.speed).ToString();




        if (carScript.carSetting.automaticGear)
        {

            if (gearst > 0 && carScript.speed > 1)
            {
                CarUI.GearText.color = Color.green;
                CarUI.GearText.text = gearst.ToString();
            }
            else if (carScript.speed > 1)
            {
                CarUI.GearText.color = Color.red;
                CarUI.GearText.text = "R";
            }
            else
            {
                CarUI.GearText.color = Color.white;
                CarUI.GearText.text = "N";
            }

        }
        else
        {

            if (carScript.NeutralGear)
            {
                CarUI.GearText.color = Color.white;
                CarUI.GearText.text = "N";
            }
            else
            {
                if (carScript.currentGear != 0)
                {
                    CarUI.GearText.color = Color.green;
                    CarUI.GearText.text = gearst.ToString();
                }
                else
                {

                    CarUI.GearText.color = Color.red;
                    CarUI.GearText.text = "R";
                }
            }

        }





        thisAngle = (carScript.motorRPM / 20) - 175;
        thisAngle = Mathf.Clamp(thisAngle, -180, 90);

        CarUI.tachometerNeedle.rectTransform.rotation = Quaternion.Euler(0, 0, -thisAngle);
        CarUI.barShiftGUI.rectTransform.localScale = new Vector3(carScript.powerShift / 100.0f, 1, 1);

    }


    private Config configScript;
    private GameObject carLoaded;
    private GameObject carGhost;
    void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        if (GameObject.Find("Client") || GameObject.Find("Server"))
        {
            Debug.Log("Client or Server is existent");
            for (int i = 0; i < 4; i++)
            {
                Debug.Log("car #" + i + " is " + configScript.playerCars[i]);
                if (configScript.playerCars[i] > 0)
                {
                    if (configScript.mapSelector == 2)
                    {
                        if (i != int.Parse(configScript.selfName) - 1)
                        {
                            carLoaded = Instantiate(configScript.cars2bots[configScript.playerCars[i] - 1], new Vector3(940.17f + i * 2.5f, 3.5f, 376.6745f), Quaternion.identity);
                            carLoaded.name = (i + 1).ToString();
                        }
                        else
                        {
                            carLoaded = Instantiate(configScript.cars2[configScript.playerCars[i] - 1], new Vector3(940.17f + i * 2.5f, 3.5f, 376.6745f), Quaternion.identity);
                            carLoaded.name = (i + 1).ToString();
                            GameObject.Find("camera").GetComponent<VehicleCamera>().target = carLoaded.transform;
                        }
                    }
                    else
                    {
                        if (i != int.Parse(configScript.selfName) - 1)
                        {
                            carLoaded = Instantiate(configScript.cars1bots[configScript.playerCars[i] - 1], new Vector3(517 + i * 6.18f, 0.5f, 223.3f), Quaternion.Euler(0,90,0));
                            carLoaded.name = (i + 1).ToString();
                        }
                        else
                        {
                            carLoaded = Instantiate(configScript.cars1[configScript.playerCars[i] - 1], new Vector3(517 + i * 6.18f, 0.5f, 223.3f), Quaternion.Euler(0,90,0));
                            carLoaded.name = (i + 1).ToString();
                            GameObject.Find("camera").GetComponent<VehicleCamera>().target = carLoaded.transform;
                        }
                    }
                }
            }
        }
        else
        {
            if (configScript.mapSelector == 2)
                carLoaded = Instantiate(configScript.cars2[configScript.carSelector - 1], new Vector3(947.61f, 3f, 377.3556f), Quaternion.identity);
            else
                carLoaded = Instantiate(configScript.cars1[configScript.carSelector - 1], new Vector3(537.3964f, 1.5f, 224.3f), Quaternion.Euler(0, 90, 0));
            GameObject.Find("camera").GetComponent<VehicleCamera>().target = carLoaded.transform;
        }
        if (configScript.modeSelector == 1)
        {
            if (configScript.mapSelector == 2)
                carGhost = Instantiate(configScript.cars2ghost[configScript.carSelector - 1], new Vector3(947.61f, 3f, 377.3556f), Quaternion.identity);
            else
                carGhost = Instantiate(configScript.cars2ghost[configScript.carSelector - 1], new Vector3(537.3964f, 1.5f, 224.3f), Quaternion.Euler(0, 90, 0));
            
            carLoaded.AddComponent<Save>();
        }
        carScript = (VehicleControl)target.GetComponent<VehicleControl>();

        myRigidbody = target.GetComponent<Rigidbody>();

        cameraSwitchView = carScript.carSetting.cameraSwitchView;

    }




    void Update()
    {

        if (!target) return;


        carScript = (VehicleControl)target.GetComponent<VehicleControl>();



        if (Input.GetKeyDown(KeyCode.G))
        {
            RestCar();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            PoliceLightSwitch();
        }


        if (restTime != 0.0f)
            restTime = Mathf.MoveTowards(restTime, 0.0f, Time.deltaTime);




        ShowCarUI();

        GetComponent<Camera>().fieldOfView = Mathf.Clamp(carScript.speed / 10.0f + 60.0f, 60, 90.0f);



        if (Input.GetKeyDown(KeyCode.C))
        {
            Switch++;
            if (Switch > cameraSwitchView.Count) { Switch = 0; }
        }



        if (Switch == 0)
        {
            // Damp angle from current y-angle towards target y-angle

            float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x,
           target.eulerAngles.x + Angle, ref xVelocity, smooth);

            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            target.eulerAngles.y, ref yVelocity, smooth);

            // Look at the target
            transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);

            var direction = transform.rotation * -Vector3.forward;
            var targetDistance = AdjustLineOfSight(target.position + new Vector3(0, height, 0), direction);


            transform.position = target.position + new Vector3(0, height, 0) + direction * targetDistance;


        }
        else
        {

            transform.position = cameraSwitchView[Switch - 1].position;
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraSwitchView[Switch - 1].rotation, Time.deltaTime * 5.0f);

        }

    }



    float AdjustLineOfSight(Vector3 target, Vector3 direction)
    {


        RaycastHit hit;

        if (Physics.Raycast(target, direction, out hit, distance, lineOfSightMask.value))
            return hit.distance;
        else
            return distance;

    }


}
