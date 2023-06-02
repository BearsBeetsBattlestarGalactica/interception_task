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

//This script performs the actual experiment procedure. To understand what is happening here I highly recommend the
//tutorial on https://www.youtube.com/watch?v=1GGXz5XwPkk. To explain every step in this code would be way to complex so I will just give short overviews of the
//different sections. Every step here is taken from the tutorial.


public class experimentalSetup : MonoBehaviour
{
    //Set up all the game objects that are included into the experiment. These can then be assigned in the game inspector
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


    //initialie the trial with the starting positions of the car and the target
    public void  drivingTaskTrial(Trial trial)
    {
        carStartingPosition = car.transform.position;
        poleStartingPosition = pole.transform.position;
        StartCoroutine(drivingTaskSequence(trial));
    }

    //Here the real experiment magic happens. The Enumerator object can be understand as a procedure protocol of the experiment where each step is worked through step by step.
    //It is initiated by a trial so that every trial follows exactly this procedure.
    IEnumerator drivingTaskSequence(Trial trial)
    {
        
        //Here we initialize the trial with its settings. These settings are set in the UXF script and can be modified in the game inspector
        int condition = trial.settings.GetInt("condition");
        int targetspeed = trial.settings.GetInt("targetspeed");

        //trial.result creates a row entry in the corresponding csv file that is created in the background when the experiments starts and saves the values
        trial.result["condition"] = condition;
        trial.result["targetspeed"] = targetspeed;

        //The experimentator has no way of checking values while the experiment runs because the Occulus Rift crashes everytime a gameobject is being inspected.
        //So the console output is a workaround for the important variables.
        Debug.Log("speed is"+ targetspeed);
        Debug.Log("condition is"+ condition);
        
        //The condition variable is part of the UXF script. For more information go to the script. 
        //The 0 condition represents the condition for the practice trials. In the practice block participants see either one of the experiment test conditions.
        //with a probability of 50%.
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


        
        //Rotation of the area in the beginning of each trial
        float randomAngle = (float)(new System.Random().NextDouble() * 180 + 90);
        environment.transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);

        trial.result["map_rotation"] = randomAngle;

        //Connect to the steering wheel again so participants can start the trial with pressing a button on it
        LogitechGSDK.LogiControllerPropertiesData actualProperties = new LogitechGSDK.LogiControllerPropertiesData();
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);

        //In this section we reset every game object to the starting positon again because they have been moved in the last trial

        //coordinates of the target
        float x_target = -12f;
        float z_target = 70f;


        float carTargetDistance = Vector2.Distance(new Vector2(0.0f, 0.0f),new Vector2(x_target, z_target)); //this variable is used to check the distance between the car and the target
        
        //reset the car object to the origin of the map and the target to its starting position according to the coordinates above
        car.transform.position = Vector3.zero;
        car.transform.rotation = Quaternion.identity;
        car.GetComponent<Rigidbody>().velocity = Vector3.zero;
        car.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        pole.transform.position = new Vector3(x_target,3f,z_target);

        //define the parameters that describe the conditions for ending a trial
        float minDist = 4f;//distance between car and target when a hit is registered
        timeLeft = 30f;//time of a trial
        
        Time.timeScale = 0f; // Pause the game when the canvas is shown
        textObject.gameObject.SetActive(true); // Show the text canvas that contains the instructions to start the next trial

        //the WaitUntil function is a very good way of checking certain conditions. The game is paused while it is checked every frame 
        //whether the conditions have been met or not. This is very valuable because Enumerator Objects dont have a update method
        //here we wait until the button on the steering wheel has been pressed
        yield return new WaitUntil(() => 
        
           LogitechGSDK.LogiGetStateUnity(0).rgbButtons[4] == 128
            
        );

        //after hitting the button the instruction text disappears
        textObject.gameObject.SetActive(false); // Hide the text canvas
        Time.timeScale = 1f; // Resume the game

        //Now the part of the experiment starts in which objects start moving.


        //The target does not move until the car crosses the starting line
        pole.GetComponent<poleMoving>().speed = 0;

        car.GetComponent<CarMotionControl>().behindTheStartLine = false;

        //Wait until the car crosses the starting line. We set the minimal distance to 1m because the distance function used the center of objects
        yield return new WaitUntil(() => 
        
            Vector3.Distance(car.transform.position, startLine.transform.position) < 1
            
        );

        car.GetComponent<CarMotionControl>().behindTheStartLine = true;

        //after the car crossed the finish line the target starts moving and the car can be steered
        pole.GetComponent<poleMoving>().speed = targetspeed;

        //Now the trial is running and we wait for one of the condition that end the trial to be met.
        yield return new WaitUntil(() => 
            //Conditions to end a trial: target is hit / time is over / car missed the target
            Vector3.Distance(car.transform.position, pole.transform.position) <minDist | timeLeft <= 0 | car.transform.position.z > pole.transform.position.z+1
            
        );

        //here we safe the value for the target being hit or not
        bool hit = false;
        if(Vector3.Distance(car.transform.position, pole.transform.position)<minDist)
        {
            hit = true;
        }

        trial.result["hit"] = hit;

        trial.End();
        

       
    }
    
    void Update(){
        //this update method implements the timer. In every frame the duration of the frame is being subtracted from the time left
        timeLeft -= Time.deltaTime; 
    }
    
    //if the last trial has ended the time is being stopped and the session ends
    public void EndIfLastTrial(Trial trial)
    {
        if(trial == Session.instance.LastTrial)
        {
            Time.timeScale = 0f;
            

            Session.instance.End();
        }
    }

}