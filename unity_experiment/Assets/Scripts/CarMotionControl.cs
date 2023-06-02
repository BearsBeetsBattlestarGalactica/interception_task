using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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



public class CarMotionControl : MonoBehaviour
{
      
        //initialize the variables. The public variables can be then modified from the unity inspector of the game object that has the script attached.
        public float speed;
        [Tooltip("maximaler Radeinschlag in [grad]")]
        public float radeinschlag;
        public float übersetzung;
        private Transform carPosition;
        private Transform actualCarPosition;
        float tempAngle = 0;
        float deltaAngle = 0;
        float newAngle = 0;
        public bool behindTheStartLine = false;

        public uxfSetup uxfsetup;

    // Update is called once per frame
    void Update()
    {
        
        //Build a connection the logitech steering wheel (packages need to be installed!)
        //For more infos see the README file in the repository

        LogitechGSDK.LogiControllerPropertiesData actualProperties = new LogitechGSDK.LogiControllerPropertiesData();
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);

        //calculate the final rotation of the car in unity from the input of the steering wheel
        //for the engineering calculations: lenkradumdrehungen, tatsächlicheÜbersetzung, radeinschlag see https://www.colliseum.eu/wiki/Lenkübersetzung 

        //engineering calculations
        float lenkradumdrehungen = radeinschlag*übersetzung*2/360;
        float tatsächlicheÜbersetzung = übersetzung/lenkradumdrehungen;


        float angleOfWheel = rec.lX; //get input from the steering wheel how many degrees it is turned
        float maximalX = 32768;//maximal possible value of the steering wheel software
        float angleOFWheelpercentage =angleOfWheel/maximalX;// get the proportionate rotation of the steering wheel
        float maxDegreeOfSteer = 110;// maximalX is reached when the steering wheel is turned about 110 degree 


        float angle = angleOFWheelpercentage*maxDegreeOfSteer/tatsächlicheÜbersetzung;// output angle that the car object will turn in unity

        angle = Mathf.Clamp(angle, -radeinschlag, radeinschlag);//clamp the maximal possible angle by the radeinschlag

        //This is the formulate to update the steering angle of the car object in each frame
        //angle: actual angle of steering
        //tempAngle: angle from last frame
        //deltaAngle: difference between the actual angle and the angle from last frame
        //newAngle: actual angle of the car

        deltaAngle = angle - tempAngle;
        newAngle = newAngle + Time.deltaTime * deltaAngle;

        //In this section we calculate the actual movement of the car object in unity. Movement and rotation is based on Vector3 objects in unity
     
        float angleToRad = (float)(System.Math.PI / 180) * newAngle; //transform degree to radiant
        Vector3 steeringVec = new Vector3((float)System.Math.Sin(angleToRad), 0, (float)System.Math.Cos(angleToRad));//Vector3 for the movement 
        Vector3 steeringVecRotation = new Vector3(0, angleToRad, 0);//Vector3 for the rotation

    
        //In this section the movement that we calculated from the user input is being combined with the actual car object
        //The Time.timeScale function controls how fast time in the game runs where 1 is normal speed and 0 is a time stop. 
        
        if(Time.timeScale == 1f & behindTheStartLine == true) //allow steering only while game is not paused and only if the car is behind the starting line
        {
            /*
            //This section might be of interest for another experiment, where the subject can control the speed of the vehicle via the pedals. For now we dont need it


            if (rec.lY <0)
            {
            transform.Translate(steeringVec*speed* Time.deltaTime); 
            transform.Rotate(steeringVecRotation);
            }

            if (rec.lY > 500)
            {
            transform.Translate(steeringVec*-speed* Time.deltaTime);    
            }
            */

            //the transform function transforms the game object's position and rotation states
            transform.Translate(steeringVec*speed* Time.deltaTime); 
            transform.Rotate(steeringVecRotation);
        }

        //only allow car to move but not to steer until car reaches starting line
        else if(Time.timeScale == 1f ){

            //by setting all angles to 0 while the starting line has not been crossed yet the 
            //participant is not able to steer the car. Only the transformation of the position is performed (car moving straigt)
            transform.Translate(Vector3.forward*speed* Time.deltaTime);
            tempAngle = 0;
            deltaAngle = 0;
            newAngle = 0;
        }
    
           
        tempAngle = newAngle; //save the actual angle for the calculations in the next frame

        //reset steering angle after every trial so that the car does not steer into the direction of the last trial
        //In this state the game is paused via the timeScale indicating that the next trial has started.
        if(Time.timeScale == 0f){
            tempAngle = 0;
            deltaAngle = 0;
            newAngle = 0;
        }

}}
