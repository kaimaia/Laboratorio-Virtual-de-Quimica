using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private GameObject Background;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayButton.onClick.AddListener(onButtonPlayClick);
    }

    private void Jogar() {
        // Background.SetActive(false);
        SceneManager.LoadScene(1);
    }

    private void onButtonPlayClick(){
        // more functions
        Jogar();
    }
}
