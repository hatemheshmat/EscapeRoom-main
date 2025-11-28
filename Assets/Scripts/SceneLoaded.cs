using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoaded : MonoBehaviour
{
   
   [SerializeField] public  TextMeshProUGUI loadedName;
    void Start()
    {
        string playerNamee = PlayerPrefs.GetString("PLayerName");
        loadedName.text = "Welcome" + playerNamee;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
