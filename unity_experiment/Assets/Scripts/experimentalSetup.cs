using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class experimentalSetup : MonoBehaviour
{

    public GameObject car;
    public GameObject pole;
    Collider carCollider;
    Collider poleCollider;
    Vector3 carStartingPosition;
    Vector3 poleStartingPosition;
    float timeLeft;
    public Text textObject;
    public GameObject environment;
    public GameObject startLine;
    public int testcondition;



    public void  drivingTaskTrial(Trial trial)
    {
        carStartingPosition = car.transform.position;
        poleStartingPosition = pole.transform.position;
        StartCoroutine(drivingTaskSequence(trial));
    }

    
    IEnumerator drivingTaskSequence(Trial trial)
    {
        
        int condition = trial.settings.GetInt("condition");
        int targetspeed = trial.settings.GetInt("targetspeed");
        trial.result["condition"] = condition;
        trial.result["targetspeed"] = targetspeed;
        Debug.Log("speed is"+ targetspeed);
        Debug.Log("condition is"+ condition);
        
        if(condition == 0)
        {
            System.Random chooseCase = new System.Random();

            if (chooseCase.NextDouble() < 0.5)
            {
                // Code line 1
            testcondition = 1;
            }

            else
            {
             testcondition = 2;  
                
            }
        }

        Debug.Log("testcondition is"+ testcondition);


        

        float randomAngle = (float)(new System.Random().NextDouble() * 180 + 90);
        environment.transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);

        trial.result["map_rotation"] = randomAngle;


        LogitechGSDK.LogiControllerPropertiesData actualProperties = new LogitechGSDK.LogiControllerPropertiesData();
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);

        //reset to starting point
        float x_target = -12f;
        float z_target = 70f;
        float carTargetDistance = Vector2.Distance(new Vector2(0.0f, 0.0f),new Vector2(x_target, z_target));
        

        car.transform.position = Vector3.zero;
        car.transform.rotation = Quaternion.identity;
        car.GetComponent<Rigidbody>().velocity = Vector3.zero;
        car.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        pole.transform.position = new Vector3(x_target,3f,z_target);


        float minDist = 4f;
        timeLeft = 30f;
        //float targetspeed = pole.GetComponent<poleMoving>().speed;
        
        Time.timeScale = 0f; // Pause the game when the canvas is shown
        textObject.gameObject.SetActive(true); // Show the text canvas

        yield return new WaitUntil(() => 
        
           LogitechGSDK.LogiGetStateUnity(0).rgbButtons[4] == 128
            
        );

        textObject.gameObject.SetActive(false); // Hide the text canvas
        Time.timeScale = 1f; // Resume the game

        pole.GetComponent<poleMoving>().speed = 0;

        car.GetComponent<CarMotionControl>().behindTheStartLine = false;

        //yield return new WaitForSeconds(3f);

        yield return new WaitUntil(() => 
        
            Vector3.Distance(car.transform.position, startLine.transform.position) < 1
            
        );

        car.GetComponent<CarMotionControl>().behindTheStartLine = true;

        pole.GetComponent<poleMoving>().speed = targetspeed;

        //
        yield return new WaitUntil(() => 
        
            Vector3.Distance(car.transform.position, pole.transform.position) <minDist | timeLeft <= 0 | car.transform.position.z > pole.transform.position.z+1
            
        );

        //speichern ob target getroffen wurde oder nicht
        bool hit = false;
        if(Vector3.Distance(car.transform.position, pole.transform.position)<minDist)
        {
            hit = true;
        }

        trial.result["hit"] = hit;

        trial.End();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

       
    }
    
    void Update(){

        timeLeft -= Time.deltaTime; 
    }
    
   
    public void EndIfLastTrial(Trial trial)
    {
        if(trial == Session.instance.LastTrial)
        {
            /*
            car.transform.position = Vector3.zero;
            car.transform.rotation = Quaternion.identity;
            car.GetComponent<Rigidbody>().velocity = Vector3.zero;
            car.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pole.transform.position = new Vector3(-46.2f,1.4f,55.4f);
            */
            Time.timeScale = 0f;
            

            Session.instance.End();
        }
    }

}