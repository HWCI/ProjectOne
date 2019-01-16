using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class CharacterScript : MonoBehaviour
{
    public Transform parent;
    public GameObject box;
    public GameObject holdingBox;
    public int boxCount = 7;
    private bool haveBox;

    void Start()
    {
        for (int i = 0; i <= boxCount; i++)
        {
            Instantiate(box, new Vector3(Random.value * 6 - 3, 7, Random.value * 6 - 3), Quaternion.identity, parent);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (haveBox)
            {
                holdingBox.GetComponent<Collider>().enabled = true;
                holdingBox.GetComponent<Rigidbody>().AddRelativeForce((Vector3.forward)*22+(Vector3.up)*30,ForceMode.Impulse);
                holdingBox = null;
                Invoke("ResetBox", 0.3f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (holdingBox != null)
        {
            holdingBox.GetComponent<Rigidbody>().Sleep();
            holdingBox.GetComponent<Collider>().enabled = false;
            holdingBox.transform.position = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);
            holdingBox.transform.rotation = transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            if (!haveBox)
            {
                holdingBox = other.gameObject;
                haveBox = true;
            }
        }
    }

    private void ResetBox()
    {
        haveBox = false;
    }
    
}
