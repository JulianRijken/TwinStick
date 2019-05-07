using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum scene
{
    mainMenu,
    tutorialLevel,
}

public class Scenes
{
    public static List<Scene> scenesList = new List<Scene>();


}

public class Scene
{
    int sceneBuildNumber;
    scene sceneName;
}