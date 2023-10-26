using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpRoom : MonoBehaviour
{
    CapsuleCollider capsuleExpand;
    IEnumerator Start()
    {
        StartCoroutine("WholeRoom", 2.0f);
        yield return new WaitForSeconds(5);
        StopCoroutine("WholeRoom");

    }
    
        
    /*IEnumerator WholeRoom(float someParameter)
    {

    }*/
    /*[SerializeField]
    GameObject objectToInactivate;
    CapsuleCollider wholeRoom;

    private void OnTriggerEnter()
    {
        if (other.gameObject.name == "Hitbox")
        {
            VisionController lightRoom = other.transform.parent.GetComponent<VisionController>();
            Debug.Log("PICK UP MF");

            
            //lightRoom.UseConsumable();

            objectToInactivate.SetActive(false);


        }
    }*/
}
