using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System.Linq;

public class uxfSetup : MonoBehaviour
{
   // Start is called before the first frame update
    public int numberOfTrialsInBlock;
    [Tooltip("1 für condition 1, 2 für condition 2 etc.")]
    public int condition;
    

    public void Generate(Session uxfSession)
    {
        Block newBlock = uxfSession.CreateBlock(numberOfTrialsInBlock);

        //List<int> speeds = new List<int>() { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6};

        List<int> speeds = Enumerable.Repeat(Enumerable.Range(4, 3).SelectMany(x => Enumerable.Repeat(x, numberOfTrialsInBlock/3)).ToArray(), 1).SelectMany(x => x).ToList();
        Debug.Log("speeds" + speeds);

        System.Random rng = new System.Random();
        speeds = speeds.OrderBy(x => rng.Next()).ToList();
        Debug.Log(string.Join(",", speeds));


        //parameters of a trial can be configured here
        foreach (Trial trial in newBlock.trials){

           
            

            int speed = speeds[0];
            speeds.RemoveAt(0);


            trial.settings.SetValue("targetspeed", speed);
            trial.settings.SetValue("condition", condition);



            /*

            if(condition == 1){
                trial.settings.SetValue("Maximum Car Speed", 3);
                trial.settings.SetValue("Maximum Pole Speed", 5);
            }

            if(condition == 2){
                trial.settings.SetValue("Maximum Car Speed", 3);
                trial.settings.SetValue("Maximum Pole Speed", 5);
            }
            */
        }
    }
}
