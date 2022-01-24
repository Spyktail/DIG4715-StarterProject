using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole : MonoBehaviour
{
    void OnTriggerEnter2D(Collision2D other)
    {
        CarController controller = other.gameObject.GetComponent<CarController>();

        if (controller != null)
        {
            controller.WinCond(2);
        }
    }
}
