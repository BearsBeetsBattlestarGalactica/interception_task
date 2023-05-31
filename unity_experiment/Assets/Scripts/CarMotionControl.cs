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
    // Start is called before the first frame update
  
        /*
        public void speed/ acceleration: ariable für Geschwindigkeit/ Beschleunigung

        */
      
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
        
        //Verbindung zum Lenkrad aufbauen
        LogitechGSDK.LogiControllerPropertiesData actualProperties = new LogitechGSDK.LogiControllerPropertiesData();
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);

       
        float lenkradumdrehungen = radeinschlag*übersetzung*2/360;
        float tatsächlicheÜbersetzung = übersetzung/lenkradumdrehungen;
        float angleOfWheel = rec.lX;
        float maximalX = 32768;
        float angleOFWheelpercentage =angleOfWheel/maximalX;
        //Debug.Log(angleOFWheelpercentage);
        float maxDegreeOfSteer = 110;
        float angle = angleOFWheelpercentage*maxDegreeOfSteer/tatsächlicheÜbersetzung;

        //angle = (float)System.Math.Floor(angle);

        angle = Mathf.Clamp(angle, -radeinschlag, radeinschlag);


        //hier stimmt was nicht
        deltaAngle = angle - tempAngle;
        newAngle = newAngle + Time.deltaTime * deltaAngle;

        //newAngle = Mathf.Clamp(newAngle, -radeinschlag, radeinschlag);

        float angleToRad = (float)(System.Math.PI / 180) * newAngle;
        Vector3 steeringVec = new Vector3((float)System.Math.Sin(angleToRad), 0, (float)System.Math.Cos(angleToRad));
        Vector3 steeringVecRotation = new Vector3(0, angleToRad, 0);

    
        
        
        
        if(Time.timeScale == 1f & behindTheStartLine == true) //allow steering only while game is not paused
        {
            /*
            This section might be of interest for another experiment, where the subject can control the speed of the vehicle


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
            transform.Translate(steeringVec*speed* Time.deltaTime); 
            transform.Rotate(steeringVecRotation);
        }

        //only allow car to move but not to steer until car reaches starting line
        else if(Time.timeScale == 1f ){

            transform.Translate(Vector3.forward*speed* Time.deltaTime);
            tempAngle = 0;
            deltaAngle = 0;
            newAngle = 0;
        }
    
        tempAngle = newAngle;

        //reset steering angle after every trial so that the car does not steer into the direction of the last trial
        if(Time.timeScale == 0f){
            tempAngle = 0;
            deltaAngle = 0;
            newAngle = 0;
        }

}}
