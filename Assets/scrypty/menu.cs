using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public string Gra;

    public void Graj()
    {
        SceneManager.LoadScene(Gra);
    }
    
}
