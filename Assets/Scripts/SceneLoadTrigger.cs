using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject Player;
    [SerializeField] private SceneField[] sceneToLoad;
    [SerializeField] private SceneField[] sceneToUnLoad;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Player Entered Trigger");
            LoadScenes();
            UnLoadScenes();
        }
    }

    public void LoadScenes()
    {
        for (int i = 0; i < sceneToLoad.Length; i++)
        {
            bool isSceneLoaded = false; 

            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);

                if (loadedScene.name == sceneToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
            {

                SceneManager.LoadSceneAsync(sceneToLoad[i], LoadSceneMode.Additive);
            }
        }
    }
    public void UnLoadScenes()
    {
        for (int i = 0; i < sceneToUnLoad.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == sceneToUnLoad[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(sceneToUnLoad[i]);
                    
                }
            }
        }
    }
}
