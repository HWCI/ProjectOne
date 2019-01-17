using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollection : MonoBehaviour
{
    private int score;
    public int Score
    {
        get { return score; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            score++;
        }
    }

}
