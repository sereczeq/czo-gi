using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beczka : MonoBehaviour {

    public float czas = 0.2f;
	void Start () {
        GetComponent<BoxCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        czas -= Time.deltaTime;
        if (czas < 0) GetComponent<BoxCollider2D>().enabled = true;
        float rotacja = transform.rotation.eulerAngles.z;
        if (rotacja < 45 || rotacja > 360 - 45) transform.Rotate(new Vector3(0, 0, -rotacja));
        else if (rotacja < 90 + 45 && rotacja > 90 - 45) transform.Rotate(new Vector3(0, 0, -rotacja + 90));
        else if (rotacja < 180 + 45 && rotacja > 180 - 45) transform.Rotate(new Vector3(0, 0, -rotacja + 180));
        else transform.Rotate(new Vector3(0, 0, -rotacja + 270));
    }
}
