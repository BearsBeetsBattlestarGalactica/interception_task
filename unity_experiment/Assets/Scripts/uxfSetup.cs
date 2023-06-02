using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System.Linq;

//This scripts describes the "meta" structure/ logic of the UXF objects involved in the experiment. To understand what is happening here I highly recommend the
//tutorial on https://www.youtube.com/watch?v=1GGXz5XwPkk. To explain every step in this code would be way to complex so I will just give short overviews of the
//different sections. Every step here is taken from the tutorial.
public class uxfSetup : MonoBehaviour
{
   
    //initialize parameters of the experiment so that it can be modified in the inspector
    public int numberOfTrialsInBlock; //number of trials of a block
    [Tooltip("1 für condition 1, 2 für condition 2 etc.")]

    //We have three conditions: 0 = practice condition, 1 = condition with constant speed, 2 = condition with constant speed and noise
    public int condition;// condition
    

    public void Generate(Session uxfSession)
    {
        Block newBlock = uxfSession.CreateBlock(numberOfTrialsInBlock);

        //List<int> speeds = new List<int>() { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6};

        //This creates the list containing all trial conditions of a block. Above is the hard coded option
        List<int> speeds = Enumerable.Repeat(Enumerable.Range(4, 3).SelectMany(x => Enumerable.Repeat(x, numberOfTrialsInBlock/3)).ToArray(), 1).SelectMany(x => x).ToList();
        Debug.Log("speeds" + speeds);

        //Here we pseudo randomize the trials by shuffling the list object
        System.Random rng = new System.Random();
        speeds = speeds.OrderBy(x => rng.Next()).ToList();
        Debug.Log(string.Join(",", speeds));


        //parameters of a trial can be configured here
        foreach (Trial trial in newBlock.trials){

           
            
            //here we pass the parameters for every trial. 

            //take first value of the shuffled list and then remove it
            int speed = speeds[0]; 
            speeds.RemoveAt(0);


            trial.settings.SetValue("targetspeed", speed);//pass speed value
            trial.settings.SetValue("condition", condition);//pass condition value

        }
    }
}
