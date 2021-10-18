using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
public class GameMgr : MonoBehaviour 
{
    [SerializeField]
    private Tilemap _tilemap;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private UnityEngine.UI.Text _time;
    public GameObject _gameScreen;
    public List<List<MapCode>> _generatedMap;

    int _pos_front_map = 0;
    bool _isGoalTileMapSet = false;
    public Timer _GamePlaytimer;



    public static GameMgr _gamemgr;
    public GameMode _gamemode = GameMode.Normal;



// TODOそのうち消す
    public int _defaultPoint = 100;
    public GameParameter _defaultParameter;
    int _numOfExtraAddTips = 0;





    public GameState _gameState = GameState.NotPlaying;


    delegate void FunctionVar(int count, FunctionVar func);

    public GameUserExp _estimatedGetExp;




    void Awake()
    {
        _gamemgr = this.GetComponent<GameMgr>();
        _GamePlaytimer = new Timer();
        User._user.SetGameParameter(new GameParameter(100,100,100,100,100));
    }

    void FixedUpdate()
    {
        if (_gameState == GameState.WalkToStart) {
            float movesteps = 0.5f;
            if (_player.transform.position.x + movesteps > 10) {
                ManipurateUser._ManipurateUser.StopUserAnimation();
                _gameState = GameState.CountDown;
                PrepareStart(3,PrepareStart);
            } else {
            ManipurateUser._ManipurateUser.MoveUserfromExternal(movesteps,0f);
            }
        }



        if (_gameState != GameState.Playing)
            return;

        // GenerateTileMaps
        GenerateMapArroundPlayer(_player.transform.position);
        _time.text = "" + _GamePlaytimer.GetElapsedTime();

        // ■■終了条件■■
        // ConfirmGameState
        if (StageMgr._stagemgr.isReachGoal(  PlayerPosXonMap((int)_player.transform.position.x )+1  ) )
            GameClear();
    
        // MgrOtherPlayers
    }






    public void InitializeGame()
    {
        _gameState = GameState.PreStart;
        _gameScreen.SetActive(true);
        
        _estimatedGetExp = new GameUserExp();
        MovePlayerPos(new Vector3(-3.44f,0.48f,0));


        //マップの元作成 変数初期化と実際のマップ生成
        StageMgr.GenerateMapArray();
        _generatedMap = new List<List<MapCode>>();
        GenerateMapArroundPlayer(new Vector3(10, 0, 0));


        
        _gameState = GameState.WalkToStart;
    }



    private void PrepareStart(int count, FunctionVar func)
    {
        switch(count) {
            case 3:
                UIMgr._uimgr._screenSimpleText.DisplayOn();
                UIMgr._uimgr._screenSimpleText.SetText(count.ToString());
                StartCoroutine(DelayCoroutine(1, () => func(count-1, func)));
            break;
            case 2:
            case 1:
                UIMgr._uimgr._screenSimpleText.SetText(count.ToString());
                StartCoroutine(DelayCoroutine(1, () => func(count-1, func)));
            break;
            case 0:
                UIMgr._uimgr._screenSimpleText.SetText("GO !!");
                _gameState =GameState.Playing;
                _GamePlaytimer.RecordStart();
                StartCoroutine(DelayCoroutine(1, () => {  UIMgr._uimgr._screenSimpleText.DisplayOFF();  }));
            break;
        }

    }



    public void GameClear(){
        UIMgr._uimgr._screenSimpleText.DisplayOn();
        UIMgr._uimgr._screenSimpleText.SetText("GOAL !!");

        _GamePlaytimer.RecordStop();
        _estimatedGetExp.GoalCup();
        _gameState = GameState.Finish;
        StartCoroutine(DelayCoroutine(3, () => {
            UIMgr._uimgr._screenSimpleText.DisplayOFF();
            ProcessGameResult();   
            }));   
    }

    private void ProcessGameResult() {
        // userクラスで経験値も扱う
        User._user.UpdateExp(_estimatedGetExp);


        ClearGameState();
        
        MenuMgr._menumgr.InitializeMenu("Result");
        _gameScreen.SetActive(false);
    }
    private void ClearGameState() {
        MovePlayerPos(new Vector3(-3.44f,0.48f,0));
        _generatedMap.Clear();
        _isGoalTileMapSet = false;
        _pos_front_map = 0;
    }





    // 10,0,0
    private void MovePlayerPos(Vector3 pos)
    {
        _player.transform.position = pos;
    }


    //  DepreCated
    public void AddExtraMapTip () {
        if (_isGoalTileMapSet)
            return;
        var tiles = StageMgr._stagemgr._stage._mappings.Mappings[MapCode.WaterCource];
        
        for ( int k = 0; k < tiles.Count; k++) {
            Tile tile = tiles[k];
            _tilemap.SetTile(new Vector3Int(_pos_front_map, 0, 0), tile);
        }
        _pos_front_map++;
        _numOfExtraAddTips++;

        _generatedMap.Add(new List<MapCode>());
        _generatedMap[_generatedMap.Count - 1].Add(MapCode.WaterCource);
    }


    public List<MapCode> GetNowTileMapCodes () {
        return _generatedMap[ (int)_player.transform.position.x ];
    }


    // _generatedMap に MacCode のリスト p を追加
    private void GenerateMapArroundPlayer(Vector3 playerpos )
    {
        int playerpos_x = (int)playerpos.x;
        
        if (_pos_front_map > playerpos_x + 8)
            return;

        for (int i = _pos_front_map; i < playerpos_x + 8; i++ ) {
            int i_n = i - _numOfExtraAddTips;
            _generatedMap.Add(new List<MapCode>());
            for (int j = 0; j < StageMgr._stagemgr.stageArray[i_n].Count;j++) {

                Maptip p = StageMgr._stagemgr.stageArray[i_n][j];
                var tiles = StageMgr._stagemgr._stage._mappings.Mappings[p._mapcode];
                
                for ( int k = 0; k < tiles.Count; k++) {
                    Tile tile = tiles[k];
                    _tilemap.SetTile(new Vector3Int(i, p._height + k, 0), tile);
                }
                if (p._mapcode == MapCode.Goal1)
                    _isGoalTileMapSet = true;

                _generatedMap[_generatedMap.Count - 1].Add(p._mapcode);
            }
        }
        _pos_front_map = playerpos_x + 8;
    }


    // マップ上のPosを示す
    public int PlayerPosXonMap(int x) {
        return x - _numOfExtraAddTips;
    }


    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
    
}
