using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCarValues : MonoBehaviour
{
   
   
    public Transform GameObject;

    
    // Update is called once per frame
    void Update()
    {
      float Rotation;
      if(GameObject.eulerAngles.y <= 180f)
      {
          Rotation = GameObject.eulerAngles.y;
      }
      else
      {
          Rotation = GameObject.eulerAngles.y - 360f;
      }

    //Debug.Log("the real rotation is: "+ -Rotation);

   
    }
}
