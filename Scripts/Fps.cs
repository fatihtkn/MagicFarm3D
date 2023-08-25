using UnityEngine;
using TMPro;

public class Fps : MonoBehaviour
{
    public TMP_Text tmp;

    private void Update()
    {

        float fps = 1 / Time.unscaledDeltaTime;
        tmp.text = fps.ToString("0");


    }
}
