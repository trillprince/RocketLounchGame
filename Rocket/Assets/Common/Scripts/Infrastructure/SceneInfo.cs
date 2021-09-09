namespace Common.Scripts.Infrastructure
{
    public static class SceneInfo
    {
        public enum SceneName
        {
            Initial,
            Menu,
            LaunchScene
        }

        public static string GetSceneName(SceneName sceneName)
        {
            return sceneName.ToString();
        }
    }
}