using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZNISZCZ : MonoBehaviour
{

    public float czas;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Zniszcz", czas);
    }

    void Zniszcz()
    {
        Destroy(gameObject);
    }
}
