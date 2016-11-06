using UnityEngine;
using System.Collections;

public class Collision2 : MonoBehaviour {

    Vector3 spawnPoint;

    void Start()
    {
        GameObject spawnObject = GameObject.FindGameObjectWithTag("Untagged");
        spawnPoint = this.transform.position;
    }
    public GameObject Cube;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Death")
        {
            transform.position = spawnPoint;
        }
    }
}
