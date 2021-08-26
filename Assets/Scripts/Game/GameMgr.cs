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



    public Tile sample;
    public Tile Post;

    int _pos_front_map = 0;
    public GameState _gameState = GameState.PreStart;

    private Timer _timer;

    delegate void FunctionVar(int count, FunctionVar func);

    void Awake()
    {
        _gamemgr = this.GetComponent<GameMgr>();
        _timer = new Timer();
    }

    void FixedUpdate()
    {

        if (_gameState != GameState.Playing)
            return;
        // GenerateTileMaps
        GenerateMapArroundPlayer(_player.transform.position);
        _time.text = "" + _timer.GetElapsedTime();

        // ConfirmGameState
        if (StageMgr._stagemgr.isReachGoal(_player.transform.position) ){
            FinishGame();
        }

        // MgrOtherPlayers
    }

    public void InitializeGame()
    {
        StageMgr.GenerateMapArray();

        GenerateMapArroundPlayer(new Vector3(10, 0, 0));
        MovePlayertoStart(new Vector3(10, 0, 0));

        
        _gameState =GameState.CountDown;


        PrepareStart(3,PrepareStart);
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
    private void MovePlayertoStart(Vector3 pos)
    {
        _player.transform.position = pos;
    }


    // 現在地より前 8 マス分マップ作成する

    private void GenerateMapArroundPlayer(Vector3 playerpos )
    {
        int playerpos_x = (int)playerpos.x;
        
        if (_pos_front_map > playerpos_x + 8)
            return;

        for (int i = _pos_front_map; i < playerpos_x + 8; i++ ) {
            for (int j = 0; j < StageMgr._stagemgr.stageArray[i].Count;j++) {

                Maptip p = StageMgr._stagemgr.stageArray[i][j];
                var tiles = StageMgr._stagemgr._stage._mappings.Mappings[p._mapcode];
                
                for ( int k = 0; k < tiles.Count; k++) {
                    Tile tile = tiles[k];
                    _tilemap.SetTile(new Vector3Int(i, p._height + k, 0), tile);
                }
                

                // Debug.Log("i : " + i + "   p.height : " + p._height);
            }

        }
        _pos_front_map = playerpos_x + 8;
    }


    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
    
}
