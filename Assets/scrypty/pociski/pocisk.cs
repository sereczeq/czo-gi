using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocisk : MonoBehaviour {

    public float prędkość;
    public float ilośćOdbić;
    public float szybkostrzelność;
    public float ilePocisków;
    public float rozmiar;
    public float damage;
    public float długośćŻycia;
    public float długośćŻycia1;
    public GameObject czyj;
    public GameObject iskry;
    public GameObject łuska;

    private bool czyPoruszać = true;

	// Use this for initialization
	void Start () {
        if(łuska) Instantiate(łuska, transform.position, transform.rotation);
        czyj = transform.parent.gameObject;
        długośćŻycia1 = długośćŻycia;
        Physics2D.IgnoreLayerCollision(12, 12);
    }

    // Update is called once per frame
    void Update () {
        damage = rozmiar;
        długośćŻycia -= Time.deltaTime;
	}

    private void FixedUpdate()
    {
        if (czyPoruszać)
            GetComponent<Rigidbody2D>().velocity = transform.up * prędkość * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("czołg"))
        {
            if (collision.gameObject == czyj && długośćŻycia > długośćŻycia1 - 0.1) return;
            collision.gameObject.GetComponent<czołg>().dostałem(damage);
            transform.DetachChildren();
            Destroy(gameObject);
        }
        else
        {
            if (ilośćOdbić <= 0)
            {
                transform.DetachChildren();
                Destroy(gameObject);
            }
            ilośćOdbić--;

            //odbicie
            if(iskry)
            {
                GameObject Iskry = Instantiate(iskry, transform.position, transform.rotation);
                Iskry.GetComponent<ParticleSystem>().Play();
            }
            czyPoruszać = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 Odbicie = Vector2.Reflect(transform.up, collision.contacts[0].normal);
            float rotacja = 90 - Mathf.Atan2(Odbicie.y, Odbicie.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, -rotacja);
            czyPoruszać = true;
        }
    }
}
