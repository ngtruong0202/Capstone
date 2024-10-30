
public enum SceneIndexes
{
    MAIN = 0,
    LOGIN = 1,
    MAP1 = 2,
    MAP2 = 3,
}

public class GetSceneIndex
{
    public int GetSceneIndexFromString(string sceneName)
    {
   
        if (System.Enum.TryParse<SceneIndexes>(sceneName, true, out SceneIndexes sceneIndex))
        {
            return (int)sceneIndex;
        }
        else
        {
            return -1;
        }
    }
}

