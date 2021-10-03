using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem Instance;

    private TextMeshProUGUI _tmp;

    private bool displaying;
    private float messageExpires;
    
    // Start is called before the first frame update
    void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        Instance = this;
    }

    public void ShowMessage(string message, float displaySeconds = 10f)
    {
        _tmp.text = message;
        
        displaying = true;
        messageExpires = Time.time + displaySeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (displaying && messageExpires < Time.time)
        {
            displaying = false;
            _tmp.text = "";
        }
    }
}
