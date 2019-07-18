using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pracuję na:
// X od 2 do 26
// Y od 20 do 2



public class mapaKropki : MonoBehaviour {
    private const float opóźnienie = 0.002f;



    //spawn
    private int maxDługość = 5;
    private bool dwójka = false;
    private int X;
    private int Y;
    //szukanie
    private int góra = 3, prawo = 3, dół = 3, lewo = 3, ileBrzegów = 8;
    private int ostatniaX = 2;
    private int ostatniaY = 20;
    //ileWolnych
    private int[] wolneX = new int [4];
    private int[] wolneY = new int [4];
    //Kropki
    private bool[,] kropki = new bool[29,23]; // 24 na 18 faktycznych
    public GameObject ściana;
    public GameObject granice;
    public GameObject[] czołgi;

	void Start () {

        GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().Spawnować = false;

        //ustawianie granic na prawdy coby się tam nic nie chciało spawnić
        for(int y = 22; y>=0; y-=2)
        {     
            kropki[0, y] = true;
            kropki[28, y] = true;
        }
        for (int x=0; x<=28; x+=2)
        {
            kropki[x, 0] = true;
            kropki[x, 22] = true;
        }

        //ustawianie całej reszty na fałsz czyli puste
        for (int y=20; y>=2; y -= 2)
        {
            for(int x=4; x<=26; x += 2)
            {
                kropki[x, y] = false;
            }
        }
        Invoke("szukanie", opóźnienie);
	}
  
    public void szukanie()
    {
        Invoke("szukanie1", opóźnienie);
    }
    public void szukanie1()
    {
        maxDługość = 5;
        if (ileBrzegów > 0)
        {
            brzegi();
            return;
        }
        else
        {
            bool znalazłem = false;
            for (int y = ostatniaY; y >= 2; y -= 2)
            {
                for (int x = ostatniaX; x <= 26; x += 2)
                {
                    if (kropki[x, y] == false)
                    {
                        X = x;
                        Y = y;
                        ostatniaX = x;
                        ostatniaY = y;
                        znalazłem = true;
                        break;
                    }
                }
                if (znalazłem) break;
                else ostatniaX = 2;
            }
            if (znalazłem) spawn();
            else
            {
                Invoke("skończyłem", opóźnienie);
            }
        }
    }
    public void brzegi()
    {
        Invoke("brzegi1", opóźnienie);
    }
    public void brzegi1()
    {
        bool czyMogę = true;
        ileBrzegów--;
        switch (Random.Range(0, 4))
        {
            case 0:
                if (góra > 0)
                {
                    X = Random.Range(1, 13) * 2;
                    Y = 22;
                    if (!kropki[X, Y - 2])
                    {
                        maxDługość--;
                        Instantiate(ściana, new Vector3(X, Y - 1, 0), Quaternion.Euler(0, 0, 90));
                        kropki[X, Y - 2] = true;
                        Y -= 2;
                    }
                    else czyMogę = false;
                    góra--;
                }
                else czyMogę = false;
                break;
            case 1:
                if (prawo > 0)
                {
                    X = 28;
                    Y = Random.Range(1, 10) * 2;
                    if (!kropki[X - 2, Y])
                    {
                        maxDługość--;
                        Instantiate(ściana, new Vector3(X - 1, Y, 0), Quaternion.Euler(0, 0, 0));

                        kropki[X - 1, Y] = true;
                        X -= 2;
                    }
                    else czyMogę = false;
                    prawo--;
                }
                else czyMogę = false;
                break;
            case 2:
                if (dół > 0)
                {
                    X = Random.Range(1, 13) * 2;
                    Y = 0;
                    if (!kropki[X, Y + 2])
                    {
                        maxDługość--;
                        Instantiate(ściana, new Vector3(X, Y + 1, 0), Quaternion.Euler(0, 0, 90));
                        kropki[X, Y + 2] = true;
                        Y += 2;
                    }
                    else czyMogę = false;
                    dół--;
                }
                else czyMogę = false;
                break;
            case 3:
                if (lewo > 0)
                {
                    X = 0;
                    Y = Random.Range(1, 10) * 2;
                    if (!kropki[X + 2, Y])
                    {
                        maxDługość--;
                        Instantiate(ściana, new Vector3(X + 1, Y, 0), Quaternion.Euler(0, 0, 0));

                        kropki[X + 1, Y] = true;
                        X += 2;
                    }
                    else czyMogę = false;
                    lewo--;
                }
                else czyMogę = false;
                break;
        }
        if (czyMogę) spawn();
        else szukanie();
    }

    public void spawn()
    {
        Invoke("spawn1", opóźnienie);
    }
    public void spawn1()
    {
        Debug.Log(maxDługość);
        if (maxDługość <= 0)
        {
            dwójka = false;
            szukanie();
        }
        else if (ileWolnych(X, Y) > 0)
        {
            if ((maxDługość == 2 || Random.Range(0, 3) == 0) && !dwójka)
            {
                spawnDwóch();
                return;
            }
            else
            {
                maxDługość--;
                int los = Random.Range(0, ileWolnych(X, Y));
                if (X != wolneX[los]) Instantiate(ściana, new Vector3((wolneX[los] + X) / 2, (wolneY[los] + Y) / 2), Quaternion.identity);
                else Instantiate(ściana, new Vector3((wolneX[los] + X) / 2, (wolneY[los] + Y) / 2), Quaternion.Euler(0, 0, 90));

                kropki[wolneX[los], wolneY[los]] = true;
                kropki[X, Y] = true;
                X = wolneX[los];
                Y = wolneY[los];
                spawn();
            }
        }
        else
        {
            kropki[X, Y] = true;
            szukanie();
        }
    }
    public void spawnDwóch()
    {
        if (maxDługość >= 2 && ileWolnych(X, Y) >= 2)
        {
            maxDługość -= 2;
            dwójka = true;

            int los = Random.Range(0, ileWolnych(X, Y));
            kropki[wolneX[los], wolneY[los]] = true;

            if (X != wolneX[los]) Instantiate(ściana, new Vector3((wolneX[los] + X) / 2, (wolneY[los] + Y) / 2), Quaternion.identity);
            else Instantiate(ściana, new Vector3((wolneX[los] + X) / 2, (wolneY[los] + Y) / 2), Quaternion.Euler(0, 0, 90));

            los = Random.Range(0, ileWolnych(X, Y));
            kropki[wolneX[los], wolneY[los]] = true;

            if (X != wolneX[los]) Instantiate(ściana, new Vector3((wolneX[los] + X) / 2, (wolneY[los] + Y) / 2), Quaternion.identity);
            else Instantiate(ściana, new Vector3((wolneX[los] + X) / 2, (wolneY[los] + Y) / 2), Quaternion.Euler(0, 0, 90));

            kropki[X, Y] = true;
            X = wolneX[los];
            Y = wolneY[los];
        }
        else if (ileWolnych(X,Y) < 2) dwójka = true;
        spawn();
    }
    
    public int ileWolnych(int X, int Y)
    {
        Debug.Log(X + ", " + Y);
        int ile = 0;
        if (!kropki[X, Y + 2])
        {
            wolneX[ile] = X;
            wolneY[ile] = Y + 2;
            ile++;
        }
        if (!kropki[X + 2, Y])
        {
            wolneX[ile] = X + 2;
            wolneY[ile] = Y;
            ile++;
        }
        if (!kropki[X, Y - 2])
        {
            wolneX[ile] = X;
            wolneY[ile] = Y - 2;
            ile++;
        }
        if (!kropki[X - 2, Y])
        {
            wolneX[ile] = X - 2;
            wolneY[ile] = Y;
            ile++;
        }
        return ile;
    }

    public void skończyłem()
    {
        Debug.Log("SKOŃCZYŁEM");
        GameObject[] ściany = GameObject.FindGameObjectsWithTag("ściana");
        foreach (GameObject ściana in ściany)
        {
            ściana.transform.parent = transform;
        }
        GameObject granica = Instantiate(granice, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        granica.transform.parent = transform;
        transform.name = "mapa";
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().Spawnować = true;
    }
}
