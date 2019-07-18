using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altyleriaSkrypt : MonoBehaviour {

    public GameObject pocisk;
    public GameObject czyj;
    public czołg zamień;
    public float prędkośćObrotu;
    public float zapisana;
    public float szybkostrzelność;
    public float ilePocisków;
    public float odległość = 5;
    public float mnożnikOdległości;
	void Start () {
        czyj = transform.parent.gameObject;
        zamień = czyj.GetComponent<czołg>();
        zamień.czyJeździć = false;
        zamień.czyStrzelaćBool = false;
        zapisana = zamień.prędkośćObrotu;
        zamień.prędkośćObrotu = prędkośćObrotu;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(zamień.strzał))
        {
            GameObject pocisk1 = Instantiate(pocisk, zamień.lufa.position, zamień.lufa.rotation);
            pocisk1.GetComponent<altyleria>().odległość = odległość;
            pocisk1.transform.parent = transform.parent;
            zamień.czyJeździć = true;
            zamień.czyStrzelaćBool = true;
            zamień.prędkośćObrotu = zapisana;
            Destroy(gameObject);
        }
        if(Input.GetKey(zamień.przód))
        {
            odległość += mnożnikOdległości * Time.deltaTime;
        } else if(Input.GetKey(zamień.tył))
        {
            odległość -= mnożnikOdległości * Time.deltaTime;
            odległość = Mathf.Max(odległość, 5);
        }
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(zamień.transform.position + (zamień.transform.up * odległość), 1);
    }
}
