using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRefill2 : MonoBehaviour
{
    [SerializeField]
    GameObject objectToInactivate;
    [SerializeField]
    GameObject textboxToInactivate;
    [SerializeField]
    GameObject textbox2ToInactivate;
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
            textboxToInactivate.SetActive(true);
            textbox2ToInactivate.SetActive(true);
            objectToInactivate.SetActive(false);
        }
    }
    public void RemoveText()
    {
        textboxToInactivate.SetActive(false);

    }
    public void RemoveText2()
    {
        textbox2ToInactivate.SetActive(false);

    }
}