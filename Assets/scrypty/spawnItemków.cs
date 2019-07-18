using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnItemków : MonoBehaviour
{
    public bool czySpawnować = true;
    public GameObject[] Itemki;
    public bool[,] itemki = new bool[29, 23]; // 24 na 18 faktycznych

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 20; y >= 2; y -= 2)
        {
            for (int x = 4; x <= 26; x += 2)
            {
                itemki[x, y] = false;
            }
        }
        Invoke("Spawn", Random.Range(10, 30));
    }


    void Spawn()
    {
        if (gameObject.transform.childCount < 6 && czySpawnować)
        {
            int x = Random.Range(1, 15);
            int y = Random.Range(1, 12);
            if (itemki[x, y] == false)
            {
                itemki[x, y] = true;
                GameObject Itemek = Instantiate(Itemki[Random.Range(0, Itemki.Length)], new Vector3 (x*2 - 1, y*2 - 1, 0), Quaternion.identity);
                Itemek.GetComponent<itemek>().x = x;
                Itemek.GetComponent<itemek>().y = y;
                Itemek.transform.parent = transform;
            }
        }

        Opóźnienie();
    }

    void Opóźnienie()
    {
        if(Random.Range(1,10) > 7)
        {
            Invoke("Spawn", Random.Range(3, 10));
        } else Invoke("Spawn", Random.Range(5, 20));

    }
}
