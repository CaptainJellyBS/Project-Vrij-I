using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public string nextLevel;
    public GameObject finishTextFuckYouUnity;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (nextLevel != "TempEndGame") //uggo magic string so tutorial doesn't crash.
            { SceneManager.LoadScene(nextLevel); return; }

            finishTextFuckYouUnity.SetActive(true);
        }
    }
}
