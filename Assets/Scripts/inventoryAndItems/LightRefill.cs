using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRefill : MonoBehaviour
{

    [SerializeField]
    GameObject objectToInactivate;
    [SerializeField]
    GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Hitbox")
        {
            //FuelConsumption fuel = other.transform.parent.GetComponent<FuelConsumption>();
            Debug.Log("PICK UP MF");
            //fuel.UseConsumable();

            objectToActivate.SetActive(true);

            objectToInactivate.SetActive(false);
        }
    }
}