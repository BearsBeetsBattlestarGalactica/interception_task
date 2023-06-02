using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the movement for the target
    
public class poleMoving : MonoBehaviour
{
    //initialize the values so that they can be modified in the inspector


    public float speed;
    public uxfSetup uxfsetup; //needed for the connection to other scripts
    public GameObject car;
    public int timeToMove = 6;//this variable is currently not used but can be another way of setting up the experiment
    public experimentalSetup experimentalSetup;//needed for the connection to other scripts


    private void Update()
    {
 
          //in this section we check whether we are in condition 1(constant speed) or in condition 2(constant speed with addition of noise in every frame)
          //and calculate the movement accordingly

          if(uxfsetup.condition == 1 | experimentalSetup.testcondition == 1)
          {
            transform.Translate(Vector3.right*speed* Time.deltaTime); 
          }
            

          if (uxfsetup.condition == 2| experimentalSetup.testcondition==2)
          {
           // Mean and standard deviation of the normal distribution
            double mean = 0;
            double stdDev = 7.0;
            // Create a new Random object
            System.Random random = new System.Random();

            // Generate a random value from a normal distribution by using the Mueller Transformation
            double randomValue = mean + stdDev * System.Math.Sqrt(-2.0 * System.Math.Log(random.NextDouble())) * System.Math.Cos(2.0 * System.Math.PI * random.NextDouble());
            
            // Add the random value to the value to add to
            speed = speed + Time.deltaTime*(float)randomValue;
          
            //transform the position value of the target object. Time.deltaTime measures the duration of a frame 
            transform.Translate(Vector3.right*(float)speed*Time.deltaTime);
          }

          Debug.Log(speed);
               
                
                
    }
}
