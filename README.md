# interception_task
This repository includes my bachelor's thesis experiment. You can find the Unity project in the corresponding folder and also the subject data that has been raised throughout the experiment.
## Experiment
In the [experiment folder](experiment) you can find the recorded data and all the other stuff that was needed like instructions that people received in the beginning of the experiment.
## Unity Experiment
In the [unity_experiment folder](unity_experiment) you can find the unity project that can be downloaded and run via the Unity Hub. The unity project has only been tested and run on Unity 2019.4.15f1. Make sure that you have the right version installed (the unity hub inbuild installer is very nice).
## Virtual Reality Integration
The experiment was performed on an Occulus Rift S head mounted display. To make it work you need to install the Meta Quest Software and create an account or login. This is mandatory, else the hardware will not be recognized by your computer. To set up your hardware just follow the steps from the Meta Quest software. After this you can start with the integration of your hardware into Unity. Here are the links I worked with. I am also a beginner so these were very useful:
* [Unity XR Toolkit](https://www.youtube.com/watch?v=u6Rlr2021vw&t=610s)
* [How to make a VR Game in Unity](https://www.youtube.com/watch?v=HhtTtvBF5bI): This is only the first video but the whole series was very helpful
* [Move the player](https://www.youtube.com/watch?v=a9D40kVB2JQ) 
## Steering Wheel Integration
The experiment used an Logitech Steering Wheel G29 as the input hardware for steering the car. The integration in unity was very tricky. If you are interested in which steps had to be done or you are facing the same issues as I did here are a few very helpful links:
* [DLL Hotfix](https://assetstore.unity.com/packages/tools/integration/logitech-gaming-sdk-6630/reviews) (Hotfix for the unity error that says that you have the wrong LogitechSteeringWheelEnginesWrapper.dll installed)
* [Logitech SDK Gaming Drivers](https://www.logitechg.com/de-de/innovation/developer-lab.html)
* [Workshop on how to simulate driving in unity](https://www.youtube.com/watch?v=d_AEmOWGuJ8). Hint: You can find the whole project from the workshop on the [slack channel](https://join.slack.com/t/cs-xrcommunity/shared_invite/zt-g01wtr4l-nrf0LTQ3FlDwvq3bU1npuw).
* [Logitech SDK Unity Asset](https://assetstore.unity.com/packages/tools/integration/logitech-gaming-sdk-6630)

A general hint at the end: Youtube has thousands of videos on how to make things work in unity. But keep in mind that a lot of them are very old in comparison to the unity version jungle out there and might not be a helpful video guide in the present.
## UXF Integration
The data recording and the experiment itself have been done in Unity via the [Unity Experiment Framework](https://github.com/immersivecognition/unity-experiment-framework). You can also find a small tutorial that explains everything you need to know [here](https://www.youtube.com/watch?v=1GGXz5XwPkk). I have made the experience that the discussion forum in the repository is also moderated very well if you have trouble integrating the framework into your unity project.
## Make the experiment run on your computer
After following alle the integration steps from above your system should be ready to run the experiment in Unity now. Here are the steps that are left to be done:
* Clone the repository to your local pc
* Open the Unity Hub and make sure that you have installed the right Unity version
* Click on "open" - "add project from disk" and navigate to your local repository folder. Then choose the unity_experiment folder
