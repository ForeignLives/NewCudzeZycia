using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadScript : MonoBehaviour
{

#if UNITY_EDITOR
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (LoadingSceneIntegration.otherScene > 0)
        {
            // Debug.Log("Returning again to the scene: " + LoadingSceneIntegration.otherScene);
            SceneManager.LoadScene(LoadingSceneIntegration.otherScene);
        }
    }
#endif
}