using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "statystykiGłówne", menuName = "Czołgi/statystykiGłówne")]

public class statystykiGłówne : ScriptableObject
{
    public float prędkośćMax;
    public float czasRozpędzania;

    public float prędkośćMaxTył;
    public float czasRozpędzaniaTył;

    public float czasHamowania;

    public float prędkośćObrotu;


    public float szybkostrzelność;
    public float ilośćPocisków;
    public float HP;


    public float prędkośćPocisku;
    public float ilośćOdbić;
    public float rozmiarPocisku;
    public float czasPrzeładowania;
}
