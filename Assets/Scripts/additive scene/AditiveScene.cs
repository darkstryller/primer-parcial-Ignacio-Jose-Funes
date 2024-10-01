using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AditiveScene : MonoBehaviour
{
    [SerializeField] int scene;
    private void Awake()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

}
