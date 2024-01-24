using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpCounter : MonoBehaviour
{
    [SerializeField] private float hpImageWidth = 83.4f;
    [SerializeField] private int maxNumberOfLives = 5;
    [SerializeField] private int numberOfLives = 5;

    public static HpCounter main;
    
    
    public void Awake()
    {
        main = GetComponent<HpCounter>();
    }
    public int NumOfLives
    {
        get => numberOfLives;
        private set
        {
            numberOfLives = value;
            if (numberOfLives <= 0)
            {
                SceneManager.LoadScene("Assets/Scenes/GameOver.unity");
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(hpImageWidth * numberOfLives, hpImageWidth);
            }
        }
    }
    public void RemoveLife()
    {
        NumOfLives--;
    }
}