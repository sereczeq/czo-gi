using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class płytka : MonoBehaviour {

    public bool dotykam = false;
    public GameObject druga;
    public GameObject spawnerŚcian;
    public int max;
    public float większeOd;

    public GameObject[] spawnery;

	void Start () {
        Invoke("zamiana", 0.1f);
	}
	
	public void zamiana()
    {
        int y = 3;
        foreach (GameObject spawner in spawnery)
        {
            if (spawner == null)
            {
                return;
            }
            if (y > 1 && Random.Range(0, max) > większeOd)
            {
                spawner.GetComponent<spawner>().zamiana();
                y--;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("płytka"))
        {
            dotykam = true;
            druga = collision.gameObject;
        }
    }
}
