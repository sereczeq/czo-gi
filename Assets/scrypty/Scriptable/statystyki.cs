using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "statystyki", menuName = "Czołgi/statystyki")]
public class statystyki : ScriptableObject {

    public statystykiGłówne statystyka;

    public float prędkośćMax;
    public float czasRozpędzania;

    public float prędkośćMaxTył;
    public float czasRozpędzaniaTył;

    public float czasHamowania;

    public float prędkośćObrotu;


    public float szybkostrzelność;
    public float ilośćPocisków;

    public float armor;
    public float HP;


    public float prędkośćPocisku;
    public float ilośćOdbić;
    public float rozmiarPocisku;
    public float czasPrzeładowania;


    public GameObject czyj;

    public bool ustawiona;

    public void OnEnable()
    {
        ustawiona = false;
    }

    public void ustawHP()
    {
        HP = statystyka.HP + armor;
    }

    public void ustaw()
    {

        if (!ustawiona)
        {
            ustawiona = true;

            HP = statystyka.HP + armor;

            prędkośćMax = statystyka.prędkośćMax;
            czasRozpędzania = statystyka.czasRozpędzania;

            prędkośćMaxTył = statystyka.prędkośćMaxTył;
            czasRozpędzaniaTył = statystyka.czasRozpędzaniaTył;

            czasHamowania = statystyka.czasHamowania;

            prędkośćObrotu = statystyka.prędkośćObrotu;

            szybkostrzelność = statystyka.szybkostrzelność;
            ilośćPocisków = statystyka.ilośćPocisków;


            prędkośćPocisku = statystyka.prędkośćPocisku;
            ilośćOdbić = statystyka.ilośćOdbić;
            rozmiarPocisku = statystyka.rozmiarPocisku;
            czasPrzeładowania = statystyka.czasPrzeładowania;
        }
    }
}