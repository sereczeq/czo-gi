using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altyleria : MonoBehaviour {

    public float damage;
    public float odległość;
    public Vector2 pozycjaPoczątkowa;
    private BoxCollider2D kolider;

	void Start () {
        kolider = GetComponent<BoxCollider2D>();
        pozycjaPoczątkowa = transform.position;
	}
	

	void Update () {
        if (Vector2.Distance(pozycjaPoczątkowa, transform.position) < odległość) kolider.enabled = false;
        else
        {
            wybuch();
        }
        if(Vector2.Distance(pozycjaPoczątkowa, transform.position) < odległość/2)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0);
        } else transform.localScale -= new Vector3(0.05f, 0.05f, 0);
    }

    public float zasieg;
    public void wybuch()
    {
        Collider2D[] trafione = Physics2D.OverlapCircleAll(transform.position, zasieg);
        foreach(Collider2D trafiony in trafione)
        {
            if(trafiony.CompareTag("czołg"))
            {
                Debug.Log("trafiłem");
                trafiony.gameObject.GetComponent<czołg>().dostałem(damage);
            }
        }
        Destroy(gameObject);
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, zasieg);
    }
    */
}
