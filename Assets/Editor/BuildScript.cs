using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    // Метод для сборки WebGL
    public static void BuildWebGL()
    {
        // Указываем путь для сохранения билда
        string buildPath = "build/WebGL";

        // Настройки для WebGL
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetEnabledScenes(), // Получаем все включенные сцены
            locationPathName = buildPath,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        // Запускаем сборку
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    // Метод для сборки Android
    public static void BuildAndroid()
    {
        // Указываем путь для сохранения билда
        string buildPath = "build/Android/myGame.apk";

        // Настройки для Android
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetEnabledScenes(), // Получаем все включенные сцены
            locationPathName = buildPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // Запускаем сборку
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    // Вспомогательный метод для получения всех включенных сцен
    private static string[] GetEnabledScenes()
    {
        var scenes = new System.Collections.Generic.List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenes.Add(scene.path);
            }
        }
        return scenes.ToArray();
    }
}