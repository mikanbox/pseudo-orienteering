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
    private Tile _titleTile;
    [SerializeField]
    private Tile _titleBaseTile;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private UnityEngine.UI.Text _time;
    public GameObject _gameScreen;
    public List<List<MapCode>> _generatedMap;

    int _pos_front_map = 0;
    // bool _isGoalTileMapSet = false;
    public Timer _GamePlaytimer;
    public float _totalmovedistance = 0;



    public static GameMgr _gamemgr;
    public GameMode _gamemode = GameMode.Normal;
    public int _selectedcupid=0;



// TODOそのうち消す
    public int _defaultPoint = 100;
    public GameParameter _defaultParameter;
    int _numOfExtraAddTips = 0;







    public GameState _gameState = GameState.NotPlaying;

    delegate void FunctionVar(int count, FunctionVar func);

    public GameUserExp _estimatedGetExp;
    public GameGlobalSaveData _gamesavedata;



    void Awake()
    {
        _gamemgr = this.GetComponent<GameMgr>();
        _GamePlaytimer = new Timer();
        InitSaveData();
        GenerateStageMap();
    }



    private void InitSaveData() {
        if (DataSaveMgr.isDataExists()){
            Debug.Log("Data exists");
            DataSaveMgr.load();
            User._user.ResetUserStateAfterClear();
        } else {
            if (GameMgr._gamemgr._gamesavedata == null)
                GameMgr._gamemgr._gamesavedata = new GameGlobalSaveData();
            if (User._user._userexp == null)
                User._user._userexp = new GameUserExp();
            if (User._user._parameter == null)
                User._user._parameter = new GameParameter();
                
            User._user.SetGameParameter(new GameParameter(100,10,10,10,10));
            this._gamesavedata.isTutorialWatchedFlags.Add(true);
            this._gamesavedata.isOpenedCups.Add(true);
        }

        DataSaveMgr.save();
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
        // ゲームプレイ中の処理 
        GenerateMapArroundPlayer(_player.transform.position);
        _time.text = "" + Math.Round( _GamePlaytimer.GetElapsedTime(), 2 )  ;

        // 終了条件
        if (StageMgr._stagemgr.isReachGoal(  PlayerPosXonMap((int)_player.transform.position.x )+1  ) )
            GameClear();
    }






    public void InitializeGame()
    {
        _gameState = GameState.PreStart;
        _gameScreen.SetActive(true);
        
        _estimatedGetExp = new GameUserExp();
        MovePlayerPos(new Vector3(-3.44f,0.48f,0));


        //マップの元作成 変数初期化と実際のマップ生成
        StageMgr.GenerateMapArray(GameGlobalClasses._gameglobal._cups.cups[_selectedcupid].terrain);
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
        UpdateResultsToMemory();
        DataSaveMgr.save();
        ManipurateUser._ManipurateUser.StopUserAnimation();

        _gameState = GameState.Finish;
        StartCoroutine(DelayCoroutine(3, () => {
            UIMgr._uimgr._screenSimpleText.DisplayOFF();
            ProcessGameResult();   
            }));
    }


    public void UpdateResultsToMemory(){
        _estimatedGetExp.GoalCup(GameGlobalClasses._gameglobal._cups.cups[_selectedcupid].difficulty);
        User._user.UpdateExp(_estimatedGetExp);

        //ステージ解放記録
        if (this._gamesavedata.isClearedCups.Count < _selectedcupid + 1)
            while(this._gamesavedata.isClearedCups.Count < _selectedcupid + 1)
                this._gamesavedata.isClearedCups.Add(false);
        this._gamesavedata.isClearedCups[_selectedcupid] = true;

        // ステージの記録
        if (this._gamesavedata._records.Count < _selectedcupid + 1)
            while(this._gamesavedata._records.Count < _selectedcupid + 1)
                this._gamesavedata._records.Add(new StargeRecord());
        
        this._gamesavedata._records[_selectedcupid]._PlayTimes++;
        this._gamesavedata._records[_selectedcupid]._TotalDistance += _totalmovedistance;


        if (this._GamePlaytimer._recordedTime < this._gamesavedata._records[_selectedcupid]._BestRecord || (this._gamesavedata._records[_selectedcupid]._BestRecord < 0))
            this._gamesavedata._records[_selectedcupid]._BestRecord = this._GamePlaytimer._recordedTime;
    

        Debug.Log("_selectedcupid :" + _selectedcupid);
        Debug.Log("record  " +this._GamePlaytimer._recordedTime);
        Debug.Log("best " +this._gamesavedata._records[_selectedcupid]._BestRecord );


        //TODO : 目標タイム達成で次ステージ解禁
        if (_GamePlaytimer._recordedTime < GameGlobalClasses._gameglobal._cups.cups[_selectedcupid].targetrecord) {
            if (_selectedcupid < this._gamesavedata.isOpenedCups.Count - 1)
                return;
            Debug.Log("new Stage opened");
            this._gamesavedata.isOpenedCups.Add(true);
        }
    }

    private void ProcessGameResult() {
        ClearGameState();
        
        MenuMgr._menumgr.InitializeMenu("Result");
        _gameScreen.SetActive(false);
    }
    private void ClearGameState() {
        _tilemap.ClearAllTiles();
        GenerateStageMap();
        MovePlayerPos(new Vector3(-3.44f,0.48f,0));

        _time.text = "00:00";
        _generatedMap.Clear();
        _pos_front_map = 0;
        _totalmovedistance = 0;
        User._user.ResetUserStateAfterClear();
    }

    // 10,0,0
    private void MovePlayerPos(Vector3 pos)
    {
        _player.transform.position = pos;
    }






    //  DepreCated
    // public void AddExtraMapTip () {
    //     if (_isGoalTileMapSet)
    //         return;
    //     var tiles = StageMgr._stagemgr._stage._mapping_tile_mapcode.Mappings[MapCode.WaterCource];
        
    //     for ( int k = 0; k < tiles.Count; k++) {
    //         Tile tile = tiles[k];
    //         _tilemap.SetTile(new Vector3Int(_pos_front_map, 0, 0), tile);
    //     }
    //     _pos_front_map++;
    //     _numOfExtraAddTips++;

    //     _generatedMap.Add(new List<MapCode>());
    //     _generatedMap[_generatedMap.Count - 1].Add(MapCode.WaterCource);
    // }


    public List<MapCode> GetNowTileMapCodes () {
        return _generatedMap[ (int)_player.transform.position.x ];
    }


    private void GenerateStageMap(){
        for (int i = -10; i < 4; i++ ) {
            _tilemap.SetTile(new Vector3Int(i, 0, 0), _titleTile);
            for (int k = 0; k < 3; k++) 
                _tilemap.SetTile(new Vector3Int(i, 0 - 1 - k, 0), _titleBaseTile);
        }
    }

    // _generatedMap に MacCode のリスト p を追加
    private void GenerateMapArroundPlayer(Vector3 playerpos )
    {
        int playerpos_x = (int)playerpos.x;
        // 描画範囲に収まっているなら戻る
        if (_pos_front_map > playerpos_x + 8)
            return;
        

        for (int i = _pos_front_map; i < playerpos_x + 8; i++ ) {
            _generatedMap.Add(new List<MapCode>());
            
            for (int j = 0; j < StageMgr._stagemgr.stageArray[i].Count;j++) {
                Maptip p = StageMgr._stagemgr.stageArray[i][j];
                var tiles = StageMgr._stagemgr._stage._mapping_tile_mapcode.Mappings[p._mapcode];
                _generatedMap[_generatedMap.Count - 1].Add(p._mapcode);
                
                for ( int k = 0; k < tiles.Count; k++) {
                    Tile tile = tiles[k];
                    _tilemap.SetTile(new Vector3Int(i, p._height + k, 0), tile);
                }
                for (int k = 0; k < 3; k++) 
                    _tilemap.SetTile(new Vector3Int(i, p._height - 1 - k, 0), StageMgr._stagemgr._stage._baseGroundTile);
                
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
