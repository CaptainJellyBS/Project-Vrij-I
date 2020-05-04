using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    int level;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entered");
        if (other.gameObject.CompareTag("Player"))
        {
            level = 2;
            SceneManager.LoadScene("assets/Scenes/Game/Scenes/Level" + level + ".unity");
        }
    }
}
