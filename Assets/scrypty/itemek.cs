using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemek : MonoBehaviour {

    //do spawnowania itemków
    public int x;
    public int y;

    public GameObject pocisk;

    public float szybkostrzelność;
    public float ilośćPocisków;

    public GameObject lufa;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("czołg"))
        {
            var ustaw = collision.gameObject.GetComponent<czołg>();
            ustaw.pocisk = pocisk;
            ustaw.ilePocisków = ilośćPocisków;
            ustaw.szybkostrzelnośćPocisków = szybkostrzelność;
            if(transform.parent)
            transform.parent.GetComponent<spawnItemków>().itemki[x, y] = false;
            //GetComponentInParent<spawnItemków>().itemki[x, y] = false;

            if (lufa) ustaw.lufaUstaw(lufa);

            Destroy(gameObject);
        }
    }
}
