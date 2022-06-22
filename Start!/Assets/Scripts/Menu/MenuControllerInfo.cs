using UnityEngine;
using UnityEngine.SceneManagement;
using RedBlueGames.Tools.TextTyper;

public class MenuControllerInfo : MonoBehaviour
{
    public TextTyper textTyper;
    void Start()
    {
        textTyper.TypeText("<color=#9b59b6>public</color> <color=#d35400>class</color> <color=yellow>Info</color> <color=blue>:</color> <color=yellow>Game</color>\n<color=yellow>{</color>\n       <color=cyan>void</color> <color=lightblue>Group</color><color=magenta>()</color>\n       <color=magenta>{</color>\n               <color=grey>Debug</color>.<color=lightblue>Log</color><color=cyan>(\"</color>\n                       <color=green>Programmer: Seyed Danial Movahed\n                       Cars by Amir Sina Rastegar\n                       Map1: Alireza Safdari Khosroshahi\n                       Map2: Amir Mohammad Salimi\n                       Thanks to Mohammad Amin Gholipour for making videos\n                       Thanks to Matin Biabanpour for helping in rendering and fixing model bugs.</color>\n               <color=cyan>\")</color><color=red>;</color>\n       <color=magenta>}</color>\n       <color=cyan>void</color> <color=lightblue>Credits</color><color=magenta>()</color>\n       <color=magenta>{</color>\n              <color=grey>Debug</color><color=lightblue>.Log</color><color=cyan>(\"</color>\n                       <color=green>Menu Music: Faux - ReVision by KrazyKen</color>\n               <color=cyan>\")</color><color=red>;</color>\n       <color=magenta>}</color>\n<color=yellow>}</color>\nPress Enter to continue...",.01f);
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            SceneManager.LoadScene("1");
        }
    }
}