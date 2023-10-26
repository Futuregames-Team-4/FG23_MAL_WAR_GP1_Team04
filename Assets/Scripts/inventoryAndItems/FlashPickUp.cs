using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashPickUp : MonoBehaviour
{
    private bool isButtonEnabled = true;

    private void OnButtonClick()
    {
        if (isButtonEnabled)
        {
            

            // Introduce a 5-second delay
            
        }
    }

    public void Use()
    {
        StartCoroutine(DelayAction(5.0f));
    }

    private IEnumerator DelayAction(float delayInSeconds)
    {
        isButtonEnabled = false;
        yield return new WaitForSeconds(delayInSeconds);

        // Re-enable the button
        isButtonEnabled = true;

        // You can add any action you want to perform after the delay here
    }
}
