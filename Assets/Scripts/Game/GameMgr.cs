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

    public static GameMgr _gamemgr;
    public GameMode _gamemode = GameMode.Normal;
    public GameObject _gameScreen;


    public int _defaultPoint = 100;
    public GameParameter _defaultParameter;





    public Tile sample;
    public Tile Post;
    public List<List<MapCode>> _generatedMap;

    int _pos_front_map = 0;
    int _numOfExtraAddTips = 0;
    bool _isGoalesSet = false;
    public GameState _gameState = GameState.NotPlaying;

    private Timer _timer;

    delegate void FunctionVar(int count, FunctionVar func);

    void Awake()
    {
        _gamemgr = this.GetComponent<GameMgr>();
        _timer = new Timer();
    }

    void FixedUpdate()
    {
        if (_gameState == GameState.WalkToStart) {
            float x = 0.5f;
            if (_player.transform.position.x + x > 10) {
                x = 10 - _player.transform.position.x;
                _gameState = GameState.CountDown;
                ManipurateUser._ManipurateUser.StopUserAnimation();
                PrepareStart(3,PrepareStart);
            } else {
                ManipurateUser._ManipurateUser.MoveUserfromExternal(x,0f);
            }
        }



        if (_gameState != GameState.Playing)
            return;
        // GenerateTileMaps
        GenerateMapArroundPlayer(_player.transform.position);
        _time.text = "" + _timer.GetElapsedTime();

        // ConfirmGameState
        if (StageMgr._stagemgr.isReachGoal(NormalizedPlayerPosX((int)_player.transform.position.x ) + 1 ) ){
            FinishGame();
        }
    
        // MgrOtherPlayers
    }




    public void InitializeGame()
    {
        _gameState = GameState.PreStart;
        _gameScreen.SetActive(true);
        _generatedMap = new List<List<MapCode>>();
        StageMgr.GenerateMapArray();

    
        GenerateMapArroundPlayer(new Vector3(10, 0, 0));

        // MovePlayerPostoStart(new Vector3(10, 0, 0));
        // UserSetting
        // User._user.SetGameParameter(new GameParameter(200,200,200,200));
        User._user.SetGameParameter(_defaultParameter);

        
        _gameState = GameState.WalkToStart;
    }

    private void PrepareStart(int count, FunctionVar func)
    {
        if (count == 3) {
            UIMgr._uimgr._screenSimpleText.DisplayOn();
            UIMgr._uimgr._screenSimpleText.SetText("3");
        }
        if (count == 2) {
            UIMgr._uimgr._screenSimpleText.SetText("2");
        }
        if (count == 1) {
            UIMgr._uimgr._screenSimpleText.SetText("1");
        }
        if (count > 0) {
            StartCoroutine(DelayCoroutine(1, () => func(count-1, func)));
        }

        if (count == 0) {

            UIMgr._uimgr._screenSimpleText.SetText("GO !!");
            StartCoroutine(DelayCoroutine(1, () => {  UIMgr._uimgr._screenSimpleText.DisplayOFF();  }));
            _gameState =GameState.Playing;
            _timer.RecordStart();
        }
    }



    public void FinishGame(){
        UIMgr._uimgr._screenSimpleText.DisplayOn();
        UIMgr._uimgr._screenSimpleText.SetText("GOAL !!");

        _gameState = GameState.Finish;

        StartCoroutine(DelayCoroutine(3, () => {  UIMgr._uimgr._fadescreen.DisplayOn();  }));
    }

    // 10,0,0
    private void MovePlayerPostoStart(Vector3 pos)
    {
        _player.transform.position = pos;
    }


    // 現在地より前 8 マス分マップ作成する

    public void AddExtraMapTip () {
        if (_isGoalesSet)
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


    private void GenerateMapArroundPlayer(Vector3 playerpos )
    {
        int playerpos_x = (int)playerpos.x;
        // int playerpos_normalized_x = NormalizedPlayerPosX(playerpos_x);
        
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
                    _isGoalesSet = true;
                // Debug.Log("i : " + i + "   p.height : " + p._height);
                _generatedMap[_generatedMap.Count - 1].Add(p._mapcode);
            }
        }
        _pos_front_map = playerpos_x + 8;
    }

    public int NormalizedPlayerPosX(int x) {
        return x - _numOfExtraAddTips;
    }


    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
    
}
