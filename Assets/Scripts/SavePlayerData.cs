using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePlayerData : MonoBehaviour
{
   [SerializeField] public TMP_InputField playerName;
    public GameObject txt;
    void Start()
    {
        txt.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void SaveDataAndLoadSceneB()
    {
        if (string.IsNullOrEmpty(playerName.text))

        {
            txt.SetActive(true);
        }
        else
        {
            string playerNamee = playerName.text;
            PlayerPrefs.SetString("PLayerName", playerNamee);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
           
    }

}
