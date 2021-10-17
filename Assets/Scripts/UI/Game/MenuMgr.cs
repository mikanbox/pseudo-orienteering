using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class MenuMgr : MonoBehaviour
{
    public static MenuMgr _menumgr;
    public Stack<MenuState> _stateStack;
    public GameObject _wholeScreen;



    [SerializeField]
    private GameObject CreateCharacter;
    [SerializeField]
    private GameObject SelectTerraion;
    [SerializeField]
    private GameObject SelectClass;

    [SerializeField]
    private GameObject SelectCup;
    [SerializeField]
    private GameObject ShowRaceDetail;
    [SerializeField]
    private GameObject ResultDetail1;
    [SerializeField]
    private GameObject ResultDetail2;

    [SerializeField]
    private GameObject TitlePage;
    [SerializeField]
    private GameObject BackGround;




    [SerializeField]
    private GameObject _plusBar;
    [SerializeField]
    private UnityEngine.UI.Text _Parameterpoint;

    [SerializeField]
    private UnityEngine.UI.Text _screenTitle;


    //　メニューの状態とGameObject のマッピング
    public Dictionary<MenuState, GameObject> _stateScreenMapping;



    [SerializeField]
    private UnityEngine.UI.ToggleGroup _toggleTerrain;
    [SerializeField]
    private UnityEngine.UI.ToggleGroup _toggleClass;

    private int _selectedTerrain = -1;
    private int _selectedClass = -1;

    MenuState _nowstate;
    


    [SerializeField]
    private GameObject _returnButton;
    [SerializeField]
    private GameObject _nextButton;
    [SerializeField]
    private GameObject _startButton;




// TODO　クラスがでかいので分割したほうが良さそう
// パラメータいじる UI と 一覧から選択する UIは分割できるのでこれを分割する
// このクラスは全体管理とし、 UIへの参照はなるべくしないようにする


    void Awake()
    {
        _menumgr = this.GetComponent<MenuMgr>();
        _stateScreenMapping = new Dictionary<MenuState, GameObject>();
        _stateScreenMapping.Add(MenuState.CreateCharacter,CreateCharacter);
        _stateScreenMapping.Add(MenuState.SelectTerraion,SelectTerraion);
        _stateScreenMapping.Add(MenuState.SelectClass,SelectClass);
        _stateScreenMapping.Add(MenuState.SelectCup,SelectCup);
        _stateScreenMapping.Add(MenuState.ShowRaceDetail,ShowRaceDetail);
        _stateScreenMapping.Add(MenuState.ResultDetail1,ResultDetail1);
        _stateScreenMapping.Add(MenuState.ResultDetail2,ResultDetail2);

        _stateStack = new Stack<MenuState>();
    }



    public void InitializeMenu(string mode_str)
    {

        GameMode mode = (GameMode)System.Enum.Parse(typeof(GameMode), mode_str);
        _wholeScreen.SetActive(true);
        TitlePage.SetActive(false);

        GameMgr._gamemgr._gamemode = mode;
        //一度前オブジェクト false に
        allSetActive(_stateScreenMapping[  MenuGlobalSetting._gameModeToMenuStateOrder[mode][0] ].transform.parent,false);
        _stateScreenMapping[  MenuGlobalSetting._gameModeToMenuStateOrder[mode][0]  ].SetActive(true);
        _nowstate = MenuGlobalSetting._gameModeToMenuStateOrder[mode][0];
        


        GameMgr._gamemgr._defaultParameter = new GameParameter(100,100,100,100);
        GameMgr._gamemgr._defaultPoint = 100;


        switch (mode) {
            case GameMode.Normal:
                GameMgr._gamemgr._defaultParameter = new GameParameter(100,100,100,100);
                GameMgr._gamemgr._defaultPoint = 100;
                break;
        }
    }





    //次の画面へ進める
    public void OnClickProgressStateButton() {
        Debug.Log("Progress");
        int count = _stateStack.Count;
        if (count == MenuGlobalSetting._gameModeToMenuStateOrder[GameMgr._gamemgr._gamemode].Count - 1) {
            if (GameMgr._gamemgr._gamemode == GameMode.Normal) {
            StartGame();
            return;
            }

            if (GameMgr._gamemgr._gamemode == GameMode.Result){
                _stateStack.Clear();
                ReturnTopPage();
                return;
            }
        }        



        _stateStack.Push( _nowstate );
        SwitchScreen(_nowstate, MenuGlobalSetting._gameModeToMenuStateOrder[GameMgr._gamemgr._gamemode][count + 1] );
        _nowstate = MenuGlobalSetting._gameModeToMenuStateOrder[GameMgr._gamemgr._gamemode][count + 1];
        _screenTitle.text = MenuGlobalSetting._menuStateToName[_nowstate];
        

        switch ( _nowstate ){
            case MenuState.CreateCharacter:
            case MenuState.ShowRaceDetail:
                BackGround.SetActive(false);
                // CreateCharacter.GetComponent<UIIncrementParameterScreenMgr>().SetUI(1000,2, new List<GameUserParameter>(){GameUserParameter.guts,GameUserParameter.speed}, new List<int>(){100,2} );
            break;
        }


        if (count == MenuGlobalSetting._gameModeToMenuStateOrder[ GameMgr._gamemgr._gamemode ].Count - 2) {
            _nextButton.SetActive(false);
            _startButton.SetActive(true);
        }
    }


    //前の画面へ戻る
    public void OnClickReturnStateButton() {
        int count = _stateStack.Count;
        Debug.Log("count:" + count);
        if (count ==0) {
            ReturnTopPage();
            return;
        }


        MenuState prestate = _nowstate;
        SwitchScreen(_nowstate, MenuGlobalSetting._gameModeToMenuStateOrder[GameMgr._gamemgr._gamemode][count-1] );
        _nowstate = MenuGlobalSetting._gameModeToMenuStateOrder[GameMgr._gamemgr._gamemode][count-1];
        _screenTitle.text = MenuGlobalSetting._menuStateToName[_nowstate];

        switch (prestate){
            case MenuState.ShowRaceDetail:
                BackGround.SetActive(true);
            break;
        }

        if (count == MenuGlobalSetting._gameModeToMenuStateOrder[ GameMgr._gamemgr._gamemode ].Count - 1) {
            _nextButton.SetActive(true);
            _startButton.SetActive(false);
        }
        _stateStack.Pop();
    }




    // 各 Toggle から呼び出すのが良さそうか
    public void GetToggleStatus() {
        MenuState state = MenuGlobalSetting._gameModeToMenuStateOrder[GameMgr._gamemgr._gamemode][_stateStack.Count];

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
        _stateScreenMapping[  MenuGlobalSetting._gameModeToMenuStateOrder[GameMgr._gamemgr._gamemode][0]  ].SetActive(false);
        _wholeScreen.SetActive(false);
    }


    private void StartGame() {
        _nextButton.SetActive(true);
        _startButton.SetActive(false);
        _returnButton.SetActive(true);

        _stateStack.Clear();
        BackGround.SetActive(true);
        _wholeScreen.SetActive(false);
        GameMgr._gamemgr.InitializeGame();
    }




    public void allSetActive(Transform form, bool enable) {
        for(int i = 0; i < form.childCount; i++ ) {
            form.GetChild(i).gameObject.SetActive(enable);
        }
    }




    private void SwitchScreen(MenuState fromstate, MenuState tostate) {
        _stateScreenMapping[ fromstate ].SetActive(false);
        _stateScreenMapping[ tostate ].SetActive(true);
    }

}
