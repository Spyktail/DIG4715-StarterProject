using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter(Collider2D other)
    {
        CarController controller = other.GetComponent<CarController>();

        if (controller != null)
        {
            controller.instructions.text = "YOU WIN!!!";
        }
    }
}
