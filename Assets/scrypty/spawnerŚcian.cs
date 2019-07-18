using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerŚcian : MonoBehaviour {

    public GameObject ściana;

    private bool zrobiłem;
	// Use this for initialization
	void Start () {
        zrobiłem = false;
        Invoke("spawn", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawn()
    {
        if(!zrobiłem)
        {
            zrobiłem = true;
            GameObject ściana1 = Instantiate(ściana, transform.position, transform.rotation) as GameObject;
            ściana1.transform.parent = transform.parent;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spawnerŚcian") && !zrobiłem)
        {
            if (!collision.GetComponent<spawnerŚcian>().zrobiłem)
            {
                Instantiate(ściana, transform.position, transform.rotation);
                zrobiłem = true;
                collision.GetComponent<spawnerŚcian>().zrobiłem = true;
                Destroy(collision);
            }
            else Destroy(gameObject);
        }
        if (collision.CompareTag("spawner"))
        {
            Destroy(collision);
        }
    }
}
