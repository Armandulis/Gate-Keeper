using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public enum SceneNamesEnum{
    MultiplayerController = 0,
    World = 20
}

public partial class SceneManager : Node2D
{
    public static SceneManager instance;

    public Dictionary<SceneNamesEnum, SceneData> sceneDictionary = new Dictionary<SceneNamesEnum, SceneData>() {
        { SceneNamesEnum.MultiplayerController, new SceneData( "res://UI/MultiplayerController/MultiplayerController.tscn", "MultiplayerController", false ) },
        { SceneNamesEnum.World, new SceneData( "res://Scenes/World.tscn", "World", false ) },
    };

    public override void _Ready() {
        instance = this;
    }

    public void ChangeScene(SceneNamesEnum sceneName )
    {
        string scenePath = sceneDictionary[sceneName].path;
        GameMaster.pauseAllowed = sceneDictionary[sceneName].pauseAllowed;

        GetTree().ChangeSceneToFile( scenePath );
    }
}
