using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
   public void ReloadGameScene()
   {
       SceneManager.LoadScene("Level 1");
   }
}
