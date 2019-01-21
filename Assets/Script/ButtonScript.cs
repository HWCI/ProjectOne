using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }
}
