using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadScript : MonoBehaviour
{


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        bool isEditor = false;
#if UNITY_EDITOR
        isEditor = true;
        
        if (LoadingSceneIntegration.otherScene > 0)
        {
            // Debug.Log("Returning again to the scene: " + LoadingSceneIntegration.otherScene);
            SceneManager.LoadScene(LoadingSceneIntegration.otherScene);
        }
#endif

        if (!isEditor)
        {
            // to nie jest edytor tylko gotowy build wiec w?acz menu
            SceneManager.LoadScene(1);
        }
    }

}