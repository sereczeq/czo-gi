using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    // do sprawdzania
    public bool Spawnować;
    public GameObject wybuch;

    public KeyCode reset;
    public KeyCode resetCzołgów;
    public GameObject[] czołgi;
    public statystyki[] statystyki;
    public bool możnaStrzelać = true;

    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(gameObject);
    }



	void Update () {
        if (Input.GetKeyDown(reset)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        spawnCzołgów();
        zabijanie();
    }

    public void spawnCzołgów()
    {
        if (Spawnować)
        {
            if (GameObject.FindGameObjectsWithTag("czołg").Length <= 1) możnaStrzelać = false;
            else możnaStrzelać = true;
            if (Input.GetKeyDown(resetCzołgów) || (GameObject.FindGameObjectsWithTag("pocisk").Length == 0 && !możnaStrzelać))
            {
                foreach (GameObject czołg in GameObject.FindGameObjectsWithTag("czołg"))
                {
                    Destroy(czołg);
                }
                int x = 1;
                int y = 1;
                foreach (GameObject czołg in czołgi)
                {
                    Instantiate(czołg, new Vector3(x, y, 0), Quaternion.identity);
                    if (x == 1 && y == 1)
                    {
                        x = 27;
                        y = 21;
                    }
                    else if (x == 27 && y == 21)
                    {
                        x = 1;
                        y = 21;
                    }
                    else if (x == 1 && y == 21)
                    {
                        x = 27;
                        y = 1;
                    }
                    else if (x == 27 && y == 1 )
                    {
                        x = 1;
                        y = 1;
                    }
                }
            }
        }
    }

    public bool nieśmiertelni = false;
    public void zabijanie()
    {
        if (nieśmiertelni) return;
        foreach (statystyki statystyka in statystyki)
        {
            if(statystyka.HP<=0 && statystyka.czyj != null)
            {
                Instantiate(wybuch, statystyka.czyj.transform.position, Quaternion.identity);
                Destroy(statystyka.czyj);
                statystyka.ustawHP();
            }
        }
    }
}
