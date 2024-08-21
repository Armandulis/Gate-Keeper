using Godot;
using System;

public class SceneData
{
    public string path {set; get; }
    public string prettyName {set; get; }
    public bool pauseAllowed {set; get; }
    
    public SceneData( string path, string prettyName, bool pauseAllowed )
    {
        this.path = path;
        this.prettyName = prettyName;
        this.pauseAllowed = pauseAllowed;
    }

}
