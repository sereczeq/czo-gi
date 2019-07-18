using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Łuska : MonoBehaviour
{
    Rigidbody2D RB;
    public float moc = 500;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.FindGameObjectWithTag("Łuski").transform;
        RB = GetComponent<Rigidbody2D>();
        RB.AddForce(transform.right * moc);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public float czas = 0;
    // Update is called once per frame
    void Update()
    {
        if (czas > 0.3 && GetComponent<BoxCollider2D>().enabled == false) GetComponent<BoxCollider2D>().enabled = true;
        else if (GetComponent<BoxCollider2D>().enabled == false) czas += 0.1f;

    }
}
