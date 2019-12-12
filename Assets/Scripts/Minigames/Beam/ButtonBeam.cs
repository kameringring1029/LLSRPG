using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBeam : MonoBehaviour
{
    public void restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
