using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    [SerializeField]
    private Button _restartButton;

    private void Awake()
    {
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(()=> { SceneManager.LoadScene("Main"); });
    }
}
