
using UnityEngine.SceneManagement;

public enum SceneName
{
    None,
    LoadingScene,
    LoginScene,
    HomeScene,
    GamePlayScene,
}

public class SceneMN
{
    static SceneName currScene = SceneName.LoadingScene;
    public static void LoadScene(SceneName scene)
    {

        SceneManager.LoadSceneAsync(scene.ToString());

        currScene = scene;

    }

    public static SceneName GetScene()
    {
        return currScene;
    }

    public static bool IsPlayingScene
    {
        get
        {
            return currScene == SceneName.GamePlayScene;
        }
    }
}
