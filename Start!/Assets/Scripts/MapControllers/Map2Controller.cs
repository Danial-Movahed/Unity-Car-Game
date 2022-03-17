using UnityEditor;
public class Map2Controller
{
    public string mode = "ghost";
    public void rename()
    {
        FileUtil.DeleteFileOrDirectory("Assets/Scripts/GhostSystem/Map2Ghost.anim");
        FileUtil.CopyFileOrDirectory("Assets/Scripts/GhostSystem/Map2Tmp.anim", "Assets/Scripts/GhostSystem/Map2Ghost.anim");
    }
}
