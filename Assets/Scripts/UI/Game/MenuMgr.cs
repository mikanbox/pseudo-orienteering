using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MenuMgr : MonoBehaviour
{
    public Stack<MenuState> _stateStack;
    public GameObject _wholeScreen;



    [SerializeField]
    private GameObject CreateCharacter;
    [SerializeField]
    private GameObject SelectTerraion;
    [SerializeField]
    private GameObject SelectClass;
    [SerializeField]
    private GameObject TitlePage;
    [SerializeField]
    private GameObject BackGround;


    [SerializeField]
    private GameObject _plusBar;
    [SerializeField]
    private UnityEngine.UI.Text _Parameterpoint;



    public Dictionary<MenuState, GameObject> _stateScreenMapping;

    public Dictionary<GameMode, List<MenuState>> _menuScreenOrder;

    [SerializeField]
    private UnityEngine.UI.ToggleGroup _toggleTerrain;
    [SerializeField]
    private UnityEngine.UI.ToggleGroup _toggleClass;

    private int _selectedTerrain = -1;
    private int _selectedClass = -1;


    void Awake()
    {
        _stateScreenMapping = new Dictionary<MenuState, GameObject>();
        _stateScreenMapping.Add(MenuState.CreateCharacter,CreateCharacter);
        _stateScreenMapping.Add(MenuState.SelectTerraion,SelectTerraion);
        _stateScreenMapping.Add(MenuState.SelectClass,SelectClass);

        _menuScreenOrder = new Dictionary<GameMode, List<MenuState>>();
        _menuScreenOrder.Add(GameMode.Normal,new List<MenuState>());
        _menuScreenOrder[GameMode.Normal].Add(MenuState.SelectTerraion);
        _menuScreenOrder[GameMode.Normal].Add(MenuState.SelectClass);
        _menuScreenOrder[GameMode.Normal].Add(MenuState.CreateCharacter);

        _stateStack = new Stack<MenuState>();
    }

    public void StartMenuSession(string mode_str)
    {
        GameMode mode = (GameMode)System.Enum.Parse(typeof(GameMode), mode_str);
        _wholeScreen.SetActive(true);
        GameMgr._gamemgr._gamemode = mode;
        _stateScreenMapping[  _menuScreenOrder[mode][0]  ].SetActive(true);
        TitlePage.SetActive(false);

        if (mode == GameMode.Normal) {
            GameMgr._gamemgr._defaultParameter = new GameParameter(100,100,100,100);
            GameMgr._gamemgr._defaultPoint = 100;
        }
    }



    public void ProgressStateButton() {
        Debug.Log("Progress");
        int count = _stateStack.Count;
        if (count == _menuScreenOrder[ GameMgr._gamemgr._gamemode ].Count - 1) {
            // _stateScreenMapping[  _menuScreenOrder[GameMgr._gamemgr._gamemode][count + 1]  ].SetActive(false);
            Debug.Log("StartGame");
            StartGame();
            return;
        }

        _stateStack.Push( _menuScreenOrder[GameMgr._gamemgr._gamemode][count] );
        _stateScreenMapping[  _menuScreenOrder[GameMgr._gamemgr._gamemode][count]  ].SetActive(false);
        _stateScreenMapping[  _menuScreenOrder[GameMgr._gamemgr._gamemode][count + 1]  ].SetActive(true);
        

        if ( _menuScreenOrder[GameMgr._gamemgr._gamemode][count+1] == MenuState.CreateCharacter) {
            BackGround.SetActive(false);
            MultiplePlusBar mp = _plusBar.GetComponent<MultiplePlusBar>();
            mp.f = changeParameter;
        }
    }

    public void changeParameter(int i, MultiplePlusBar m) {
        Debug.Log("ChangeParameter : " + i);
        switch (m._parameterType) 
        {
            case GameUserParameter.speed:
                GameMgr._gamemgr._defaultParameter._speed += i;
                GameMgr._gamemgr._defaultPoint -= i;

                if ( i == 0) {
                    int tmp = GameMgr._gamemgr._defaultParameter._speed - 100;
                    GameMgr._gamemgr._defaultParameter._speed -= tmp;
                    GameMgr._gamemgr._defaultPoint += tmp;
                }

                m.updateTexts("" + GameMgr._gamemgr._defaultParameter._speed + "p");
                break;
        }

    }



    public void ReturnStateButton() {
        int count = _stateStack.Count;
        if (count ==0) {
            ReturnTopPage();
            return;
        }
        _stateScreenMapping[  _menuScreenOrder[GameMgr._gamemgr._gamemode][count]  ].SetActive(false);
        _stateScreenMapping[  _menuScreenOrder[GameMgr._gamemgr._gamemode][count-1]  ].SetActive(true);

        if ( _menuScreenOrder[GameMgr._gamemgr._gamemode][count] == MenuState.CreateCharacter) 
            BackGround.SetActive(true);

        _stateStack.Pop();
    }




    private void GetToggleStatus(MenuState state) {
        if (MenuState.SelectClass == state) {
            UnityEngine.UI.Toggle activetoggle = _toggleClass.ActiveToggles().FirstOrDefault();
            _selectedClass = activetoggle.gameObject.GetComponent<SetValueToObject>().value;
        }

        if (MenuState.SelectTerraion == state) {
            UnityEngine.UI.Toggle activetoggle = _toggleTerrain.ActiveToggles().FirstOrDefault();
            _selectedTerrain = activetoggle.gameObject.GetComponent<SetValueToObject>().value;
        }
    }




    private void ReturnTopPage() {
        TitlePage.SetActive(true);
        _stateScreenMapping[  _menuScreenOrder[GameMgr._gamemgr._gamemode][0]  ].SetActive(false);
        _wholeScreen.SetActive(false);
    }


    private void StartGame() {

        _stateStack.Clear();
        BackGround.SetActive(true);
        _wholeScreen.SetActive(false);
        GameMgr._gamemgr.InitializeGame();
    }



}
