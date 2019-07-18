using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunScript : MonoBehaviour {

    public GameObject pocisk;
    public GameObject czyj;
    public GameObject łuska;
    public Sprite[] śruty;
    public float spread;
    public float los;
	// Use this for initialization
	void Start () {
        czyj = transform.parent.gameObject;
        var rotacja = czyj.transform.eulerAngles;
        rotacja.z -= spread;
        for (int x=5; x>=0; x--)
        {
            GameObject pocisk1 = Instantiate(pocisk, transform.position, Quaternion.Euler(rotacja));
            pocisk1.GetComponent<SpriteRenderer>().sprite = śruty[Random.Range(0,10)];
            pocisk1.transform.parent = transform.parent;
            rotacja.z += spread * 2 / 6 + Random.Range(-los, los);
            pocisk1.GetComponent<pocisk>().czyj = czyj;
        }
        Instantiate(łuska, transform.position, transform.rotation);
        Destroy(gameObject);

	}
	
}
