using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sheepButtonScript : MonoBehaviour
{
    [SerializeField] SpriteState spriteSet;
    [SerializeField] Button button;

    // Update is called once per frame
    void Start()
    {
        button.spriteState = spriteSet;
    }

    public void buttonTest()
    {
        Debug.Log("Test1234");
    }
}
