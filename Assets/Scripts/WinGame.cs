using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    Transform tf;
    public NewUseItem player;

    private void Awake()
    {
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        if (player.hasKey == true)
        {
            //if (tf.position == new Vector3((float)-8.75, (float)0, (float)-13.75))
            if (tf.position == new Vector3((float)6, (float)0, (float)-28))
            {
                Debug.Log("YOU WON");
                SceneManager.LoadScene("WinScene");
            }
            //Vector3(6, 0, -28)
        }
        
        //Vector3(-8.75, 0, -13.75)
    }
     //if (tf.position == new Vector3((float)-9.7007122, (float)0.809054792, (float)-11.1634684))
}