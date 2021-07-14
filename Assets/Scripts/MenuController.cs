using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void FaceToFace() 
    {
        SceneManager.LoadScene("1vs1");
    }
    public void AIComputer() 
    {
        SceneManager.LoadScene("AIComputer");
    }
}
