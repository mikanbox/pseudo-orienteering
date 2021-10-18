using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MenuState 
{
    SelectClass,
    SelectTerraion,
    CreateCharacter,
    SelectCup,
    ShowRaceDetail,
    ResultDetail1,
    ResultDetail2

}


public enum GameMode 
{
    Normal,
    FreeRun,
    MultiPlay,
    Result,
    HowToPlay
}


public class MenuGlobalSetting
{   
    public static Dictionary<MenuState, string> _menuStateToName = new Dictionary<MenuState, string>()
    {
    {MenuState.SelectClass,"SelectClass"},
    {MenuState.CreateCharacter,"CerateCharacter"},
    {MenuState.SelectTerraion,"SelectTerrain"},
    {MenuState.SelectCup,"SelectCup"},
    {MenuState.ShowRaceDetail,"ShowRaceDetail"},
    {MenuState.ResultDetail1,"ShowResultDetail - 1"},
    {MenuState.ResultDetail2,"ShowResultDetail - 2"},
    };

    public static Dictionary<GameMode, List<MenuState>> _gameModeToMenuStateOrder = new Dictionary<GameMode, List<MenuState>>()
    {
        {
            GameMode.Normal,
            new List<MenuState>() {
                // MenuState.SelectTerraion, MenuState.SelectClass, MenuState.CreateCharacter
                // MenuState.SelectTerraion, MenuState.SelectCup, MenuState.CreateCharacter
                // MenuState.SelectCup,MenuState.ShowRaceDetail
                MenuState.SelectCup
            }
        },
        {
            GameMode.Result,
            new List<MenuState>() {
                // MenuState.ResultDetail1,MenuState.ResultDetail2
                MenuState.ResultDetail2
            }
        },
    };
    public static Dictionary<MenuState, GameObject> _stateScreenToGameObect;

}
