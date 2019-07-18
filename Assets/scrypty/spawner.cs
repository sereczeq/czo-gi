using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    public GameObject płytka;
    private Vector2 granicaX = new Vector2(0, Screen.width);
    private Vector2 granicaY = new Vector2(0, Screen.height);

    public bool zrobiłem = false;
    public bool podałem = false;
    public GameObject spawnerŚcian;

    void Start () {
        zrobiłem = false;
        podałem = false;
        //Invoke("spawnPłytki", 1f);
        Invoke("pozycja", 0.1f);
	}

    private void Update()
    {
        if (Input.GetKeyDown("q") && !zrobiłem) spawnPłytki();
        if (Input.GetKeyDown("e"))
        {
            zrobiłem = false;
            podałem = false;
        }


    }

    public void pozycja()
    {
        Vector2 pozycjaNaEkranie = Camera.main.WorldToScreenPoint(transform.position);
        if (pozycjaNaEkranie.x < granicaX.x || pozycjaNaEkranie.x > granicaX.y
            || pozycjaNaEkranie.y < granicaY.x || pozycjaNaEkranie.y > granicaY.y)
            Destroy(gameObject);
    }

    public void spawnPłytki()
    {
        zrobiłem = true;
        GameObject płytka1 = Instantiate(płytka, pozycjaSpawnu(), transform.rotation) as GameObject;
        płytka1.GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(rotacjaTrawy());

    }

    public Vector3 rotacjaTrawy()
    {
        if (transform.rotation.eulerAngles == new Vector3(0, 0, 0))
        {
            return new Vector3(0, 0, 0);
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 0, 90))
        {
            return new Vector3(0, 0, 90);
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 0, 270))
        {
            return new Vector3(0, 0, 270);
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 0, 180))
        {
            return new Vector3(0, 0, -180);
        }
        else Debug.Log("błąd obrotu: " + gameObject);
        return new Vector3(0, 0, 0);
    }


    public Vector3 pozycjaSpawnu()
    {
        if (transform.rotation.eulerAngles == new Vector3(0, 0, 0))
        {
            return new Vector3(transform.position.x, transform.position.y + 1, 0);
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 0, 90))
        {
            return new Vector3(transform.position.x - 1, transform.position.y, 0);
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 0, 270))
        {
            return new Vector3(transform.position.x + 1, transform.position.y, 0);
        }
        else if (transform.rotation.eulerAngles == new Vector3(0, 0, 180))
        {
            return new Vector3(transform.position.x, transform.position.y - 1, 0);
        }
        else Debug.Log("błąd obrotu: " + gameObject);
        return new Vector3(0, 0, 0);
    }

    public void zamiana()
    {
        GameObject spawnerŚcian1 = Instantiate(spawnerŚcian, transform.position, transform.rotation) as GameObject;
        spawnerŚcian1.transform.parent = transform.parent;
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spawnerŚcian")) Destroy(gameObject);
        else if (collision.CompareTag("spawner"))
        {
            if(collision.gameObject.GetComponent<spawner>().zrobiłem)
            Destroy(gameObject);
        }
    }
}
