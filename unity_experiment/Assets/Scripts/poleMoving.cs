using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poleMoving : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed;
    public uxfSetup uxfsetup;
    public GameObject car;
    public int timeToMove = 6;
    public experimentalSetup experimentalSetup;

    // Update is called once per frame

    private void Update()
    {
           //deltaTime converts from units per frame to units per second (because there are different frame rates on different devices plus different loading times)
           //Vector.Direction are unit vectors 
        
        /*
          if(uxfsetup.condition == 0){

                System.Random chooseCase = new System.Random();

                if (chooseCase.NextDouble() < 0.5)
                {
                 // Code line 1
                    transform.Translate(Vector3.right*speed* Time.deltaTime); 
                }

                else
                {
                    // Code line 2
                    double mean = 0;
                    double stdDev = 7.0;
                    // Create a new Random object
                    System.Random random = new System.Random();

                    // Generate a random value from a normal distribution
                    double randomValue = mean + stdDev * System.Math.Sqrt(-2.0 * System.Math.Log(random.NextDouble())) * System.Math.Cos(2.0 * System.Math.PI * random.NextDouble());
            
                    // Add the random value to the value to add to
                    speed = speed + Time.deltaTime*(float)randomValue;
          
 
                    transform.Translate(Vector3.right*(float)speed*Time.deltaTime);
                }
                

          }

          */

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

            // Generate a random value from a normal distribution
            double randomValue = mean + stdDev * System.Math.Sqrt(-2.0 * System.Math.Log(random.NextDouble())) * System.Math.Cos(2.0 * System.Math.PI * random.NextDouble());
            
            // Add the random value to the value to add to
            speed = speed + Time.deltaTime*(float)randomValue;
          
 
            transform.Translate(Vector3.right*(float)speed*Time.deltaTime);
          }

          Debug.Log(speed);
               
                
                
    }
}
