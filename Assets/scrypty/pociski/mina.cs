using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mina : MonoBehaviour
{

    public GameObject czyj;
    public float damage;
    public float długośćŻycia;
    public float długośćŻycia1;
    public SpriteRenderer sprite;
    void Start()
    {
        czyj = transform.parent.gameObject;
        sprite = GetComponent<SpriteRenderer>();
        długośćŻycia1 = długośćŻycia;
    }

    void Update()
    {
        
        długośćŻycia -= Time.deltaTime;
        if (długośćŻycia < 0) sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("czołg"))
        {
            if (collision.gameObject == czyj && długośćŻycia > długośćŻycia1 - 2) return;
            collision.gameObject.GetComponent<czołg>().dostałem(damage);
            Destroy(gameObject);
        }

    }
}
