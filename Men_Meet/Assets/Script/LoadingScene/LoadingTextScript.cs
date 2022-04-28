using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextScript : MonoBehaviour
{
    [SerializeField] Text LoadingMainText;

    void Start() => Invoke("PlusMain", 0.25f);
    public void PlusMain()
    {
        LoadingMainText.text += ".";

        if (LoadingMainText.text.Equals("접속중.....")) 
        LoadingMainText.text = "접속중.";

        Invoke("PlusMain", 0.25f);   
    }
    public void StopAni(){
        CancelInvoke();
    }
}
