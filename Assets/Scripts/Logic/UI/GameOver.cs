using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public Image goImage;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Restart() 
    {
        StartCoroutine("Restart");
    }

    private IEnumerator RestartRutine()
    {
        goImage.enabled = true;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("IntroScene");
    }
}
