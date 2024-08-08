using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingUI : MonoBehaviour
{
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Guessr Scene");
    }
}
