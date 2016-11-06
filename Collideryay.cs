using UnityEngine;
using System.Collections;

public class Collideryay : MonoBehaviour {
    Vector3 spawnPoint;

    void Start()
    {
        GameObject spawnObject = GameObject.FindGameObjectWithTag("Finish");
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
