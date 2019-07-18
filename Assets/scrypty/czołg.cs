using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class czołg : MonoBehaviour {



    public float prędkośćObrotuAltyleria;

    public KeyCode przód;
    public KeyCode tył;
    public KeyCode lewo;
    public KeyCode prawo;
    public KeyCode strzał;

    public float HP;
    public float prędkość = 0;
    public float przyśpieszenie;
    public float przyśpieszenieTył;
    public float hamowanie;

    public float prędkośćMax;
    public float czasRozpędzania;


    public float prędkośćMaxTył;
    public float czasRozpędzaniaTył;

    public float czasHamowania;

    public float prędkośćObrotu;

    public float szybkostrzelność;
    public float ilośćPocisków;

    public bool czyJeździć = true;

    public statystyki statystyka;
    public GameObject pociskDomyślny;
    public GameObject pocisk;
    public float ilePocisków;
    public float szybkostrzelnośćPocisków;
    public Transform lufa;
    private Rigidbody2D RB;
    public bool ustawiony = false;
    public GameObject lufaObj;
    public GameObject lufaShotgun;

    // Use this for initialization
    void Start () {
        statystyka.ustawHP();
        RB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        ustawianie();
        strzelanie();
    }

    void FixedUpdate()
    {
        poruszanie();
    }
    public void poruszanie()
    {
        if (czyJeździć)
        {
            if (Input.GetKey(przód) && prędkość >= 0)
            {
                prędkość += przyśpieszenie * Time.deltaTime;
                prędkość = Mathf.Min(prędkość, prędkośćMax);
            }
            else if (Input.GetKey(tył) && prędkość <= 0)
            {
                prędkość += przyśpieszenieTył * Time.deltaTime;
                prędkość = Mathf.Max(prędkość, prędkośćMaxTył);
            }
            else if (prędkość > 0)
            {
                prędkość -= hamowanie * Time.deltaTime;
                prędkość = Mathf.Max(prędkość, 0);
            }
            else if (prędkość < 0)
            {
                prędkość += hamowanie;
                prędkość = Mathf.Min(prędkość, 0);
            }
            RB.velocity = transform.up * prędkość * Time.deltaTime;
        }
        else RB.velocity = transform.up*0;


        if (Input.GetKey(prawo) && !Input.GetKey(lewo))
        {
            RB.MoveRotation(RB.rotation - prędkośćObrotu);
        }
        else if (Input.GetKey(lewo) && !Input.GetKey(prawo))
        {
            RB.MoveRotation(RB.rotation + prędkośćObrotu);
        }
        else RB.MoveRotation(RB.rotation);
    }

    private float ileDoStrzału;
    public void strzelanie()
    {
        ileDoStrzału -= Time.deltaTime;

        if (Input.GetKeyDown(strzał))
        {
            if (pocisk == null) // jeśli pocisk jest null to używamy domyslnego pocisku
            {
                if (ileDoStrzału <= 0 && czyStrzelać() && !lufa.GetComponent<BoxCollider2D>().IsTouchingLayers(-1))
                {
                    ilośćPocisków--;
                    GameObject pocisk1 = Instantiate(pociskDomyślny, lufa.position, transform.rotation) as GameObject;
                    pocisk1.transform.parent = transform;
                    var ustaw = pocisk1.GetComponent<pocisk>();
                    ustaw.czyj = gameObject;
                    ustaw.prędkość = statystyka.prędkośćPocisku;
                    ustaw.ilośćOdbić = statystyka.ilośćOdbić;
                    ustaw.rozmiar = statystyka.rozmiarPocisku;
                    ileDoStrzału = 1 / szybkostrzelność;
                    cofajLufe();
                }
            }
            else if (ileDoStrzału <= 0 && czyStrzelać() && !lufa.GetComponent<BoxCollider2D>().IsTouchingLayers(-1))
            {
                GameObject pocisk1 = Instantiate(pocisk, lufa.position, transform.rotation) as GameObject;
                pocisk1.transform.parent = transform;
                ilePocisków--;
                if (ilePocisków <= 0)
                {
                    pocisk = null;
                    Destroy(lufaZmienna);
                    ilośćPocisków = 0;
                }
                else ileDoStrzału = 1 / szybkostrzelnośćPocisków;
            }
            if (!czyStrzelać()) Debug.Log("reload");
            else if (ileDoStrzału <= 0) Debug.Log("szybkostrzelność");
        }
    }

    public bool czyStrzelaćBool = true;
    public bool czyStrzelać()
    {
        if (ilośćPocisków <= 0)
        {
            StartCoroutine(przeładowanie());
            return false;
        }
        else if (czyStrzelaćBool) return true;
        else return false;
    }
    public bool przeładowuje = false;
    IEnumerator przeładowanie()
    {
        if(!przeładowuje)
        {
            przeładowuje = true;
            yield return new WaitForSeconds(statystyka.czasPrzeładowania);
            ilośćPocisków = statystyka.ilośćPocisków;
            przeładowuje = false;
        }
    }

    public void dostałem(float damage)
    {
        statystyka.HP -= damage;
    }

    public void ustawianie()
    {
        statystyka.czyj = gameObject;
        HP = statystyka.HP;

        if (!statystyka.ustawiona)
        {
            statystyka.ustaw();
        }
        if(!ustawiony)
        {
            ustawiony = true;

            prędkośćMax = statystyka.prędkośćMax;
            czasRozpędzania = statystyka.czasRozpędzania;

            prędkośćMaxTył = statystyka.prędkośćMaxTył;
            czasRozpędzaniaTył = statystyka.czasRozpędzaniaTył;

            czasHamowania = statystyka.czasHamowania;

            prędkośćObrotu = statystyka.prędkośćObrotu;

            przyśpieszenie = prędkośćMax / czasRozpędzania;
            przyśpieszenieTył = prędkośćMaxTył / czasRozpędzaniaTył;
            hamowanie = prędkośćMax / czasHamowania;

            szybkostrzelność = statystyka.szybkostrzelność;
            ilośćPocisków = statystyka.ilośćPocisków;
        }
    }

    public void cofajLufe()
    {
        
    }

 
    GameObject lufaZmienna;
    public void lufaUstaw (GameObject lufaNowa)
    {
        lufaZmienna = Instantiate(lufaNowa, transform.position, transform.rotation);
        lufaZmienna.transform.parent = gameObject.transform;
        lufaZmienna.GetComponent<SpriteRenderer>().color = Color.green;
        lufaZmienna.GetComponent<SpriteRenderer>().sortingLayerName = "czołg";
        lufaZmienna.GetComponent<SpriteRenderer>().sortingOrder = 10;
    }
}
