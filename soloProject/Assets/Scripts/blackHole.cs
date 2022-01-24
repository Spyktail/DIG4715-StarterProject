using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole : MonoBehaviour
{
    void OnTriggerEnter(Collider2D other)
    {
        CarController controller = other.GetComponent<CarController>();

        if (controller != null)
        {
            controller.WinCond(2);
        }
    }
}
