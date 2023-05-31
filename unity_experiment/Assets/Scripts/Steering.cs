using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Steering : MonoBehaviour
{
    public ActionBasedController m_leftController;
    public ActionBasedController m_rightController;
    public Transform m_offset;
    public Transform m_SteeringWheel;
    public Transform m_SteeringWheelChild;
    public WheelCollider m_FLwheel;
    public WheelCollider m_FRwheel;
    public WheelCollider m_BLwheel;
    public WheelCollider m_BRwheel;
    public Transform m_FLwheelTransform;
    public Transform m_FRwheelTransform;
    public float m_accelerationForce;
    public float m_breakForce;
    public float m_maxSteerAngle;


    //target is the players hand (m is hungarian notion and means membervariable. it helps finding the variables)
    private Transform m_target;
    private Vector3 m_fromVector;
    private bool m_steered;
    private float m_angleBetween;

    //onTrigger = when something touches something
    
        
    /*    
    m_target = other.transform;

    m_offset.position = m_target.position;
    m_offset.localPosition = new Vector3(m_offset.localPosition.x, 0, m_offset.localPosition.z);
    Vector3 dir = m_offset.position - transform.position;

    Quaternion rot = Quaternion.LookRotation(dir, transform.up);

    m_SteeringWheelChild.SetParent(null);
    m_SteeringWheel.rotation = rot;
    m_SteeringWheelChild.SetParent(m_SteeringWheel);
    */


    private void Update()
    {

        
        
        LogitechGSDK.LogiControllerPropertiesData actualProperties = new LogitechGSDK.LogiControllerPropertiesData();
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);

      
        /*
        //if there is a hand touching take the targets position
        if (m_target)
        {
            m_offset.position = m_target.position;

            //set the position of the steering wheel to the same position
            m_offset.localPosition = new Vector3(m_offset.localPosition.x, 0, m_offset.localPosition.z);

            //steering wheel has to look at the position 
            Vector3 dir = m_offset.position - transform.position;

            //rotation in the direction of looking
            Quaternion rot = Quaternion.LookRotation(dir, transform.up);

            //steering wheel moving animation?
            m_SteeringWheel.rotation = rot;
            if (m_steered)
            {
                //angle between where i started and where i am with the angle function of 2 vectors
                m_angleBetween = Vector3.Angle(m_fromVector, dir);

                //cross product for the perpendicular sort of angle for those 2 -> detect whether you turn to the left or to the right
                Vector3 cross = Vector3.Cross(m_fromVector, dir);
                if(cross.y <0)
                {
                    m_angleBetween = -m_angleBetween;
                }
                m_fromVector = dir;
                Debug.Log(m_angleBetween);

                //how much do you want to adjust the steering wheel
                float angle = m_FLwheel.steerAngle;
                angle += m_angleBetween/10;
                angle = Mathf.Clamp(angle, -m_maxSteerAngle, m_maxSteerAngle);
                m_FLwheel.steerAngle = angle;
                m_FRwheel.steerAngle = angle;
                AngleWheel(m_FLwheel, m_FLwheelTransform);
                AngleWheel(m_FRwheel, m_FRwheelTransform);


            }
            else
            {
                m_steered = true;
                m_fromVector = dir;
            }
        }

        
       
        m_offset.position = m_target.position;

        //set the position of the steering wheel to the same position
        m_offset.localPosition = new Vector3(m_offset.localPosition.x, 0, m_offset.localPosition.z);

        //steering wheel has to look at the position 
        Vector3 dir = m_offset.position - transform.position;

        //rotation in the direction of looking
        Quaternion rot = Quaternion.LookRotation(dir, transform.up);

        //steering wheel moving animation?
        m_SteeringWheel.rotation = rot;
        
        
        //angle between where i started and where i am with the angle function of 2 vectors
        m_angleBetween = Vector3.Angle(m_fromVector, dir);

        //cross product for the perpendicular sort of angle for those 2 -> detect whether you turn to the left or to the right
        Vector3 cross = Vector3.Cross(m_fromVector, dir);
        if(cross.y <0)
        {
            m_angleBetween = -m_angleBetween;
        }
        m_fromVector = dir;
        Debug.Log(m_angleBetween);


        */

        



        //how much do you want to adjust the steering wheel
        float angle = m_FLwheel.steerAngle;
        //angle += m_angleBetween/10;

        float angleOfWheel = rec.lX;
        float maximalX = 32768;
        float angleOFWheelpercentage =angleOfWheel/maximalX;
        //Debug.Log(angleOFWheelpercentage);
        float maxDegreeOfSteer = 110;
        angle = angleOFWheelpercentage*maxDegreeOfSteer;
        

        angle = Mathf.Clamp(angle, -m_maxSteerAngle, m_maxSteerAngle);
        m_FLwheel.steerAngle = angle;
        m_FRwheel.steerAngle = angle;
        AngleWheel(m_FLwheel, m_FLwheelTransform);
        AngleWheel(m_FRwheel, m_FRwheelTransform);
        //Debug.Log("the calculated angle is: "+ angle);

        var car = GameObject.Find("RMCarDemo_USA");
        Rigidbody rigidBody = car.GetComponent<Rigidbody>();   
        float speed =  rigidBody.velocity.x;
       
        
        if(rec.lY < 0 & rec.rgbButtons[4] == 128 )
        {
           m_BLwheel.motorTorque =-m_accelerationForce;
           m_BRwheel.motorTorque =-m_accelerationForce; 
           return;
        }

        //brake

        
        if ( rec.lY > 500)
        {
            
            m_FLwheel.brakeTorque = m_breakForce;
            m_FRwheel.brakeTorque = m_breakForce;
            m_BLwheel.brakeTorque = m_breakForce;
            m_BRwheel.brakeTorque = m_breakForce;
        }

        //no braking
        else
        {
            m_FLwheel.brakeTorque = 0;
            m_FRwheel.brakeTorque = 0;
            m_BLwheel.brakeTorque = 0;
            m_BRwheel.brakeTorque = 0;
        }

        //gas
        if (rec.lY <0)
        {
            m_BLwheel.motorTorque = m_accelerationForce;
            m_BRwheel.motorTorque = m_accelerationForce;
        }
        
        //no gas
        else
        {
            m_BLwheel.motorTorque = 0;
            m_BRwheel.motorTorque = 0;
        }
        
        


        
       
    
        
    }
    
    void AngleWheel(WheelCollider w, Transform t)
    {
        /*
        Vector3 pos;
        Quaternion rot;

        w.GetWorldPose(out pos, out rot);
        t.rotation = rot;

        */
    }
    
    
}