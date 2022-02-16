using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public int level = 0;
    public int remaining = 0;
    public float timer = -3f;
    public Text levelText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (remaining == 0 && timer >= 1f && level <= 10)
        {
            level += 1;
            levelText.text = "LEVEL: " + level;
            timer = -3f;
            if (level >= 11)
            {
                SceneManager.LoadScene("YouWin");
            }
        }
    }
}
