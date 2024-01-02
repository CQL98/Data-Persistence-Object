using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    private Text text; 
    public float timeToChange = 1.5f; 
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        InvokeRepeating("ChangueColor",0,timeToChange);
    }

    // Update is called once per frame
    void Update()
    { 
    }
    public void ChangueColor()
    {
        Color color = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat());
        text.color = color;
    }     
    public float GetRandomFloat()
    {
        return Random.Range(0.0f, 1.0f); 
    }
}
