using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ManipurateUser : MonoBehaviour
{
    private SpriteRenderer _userSprite;
    private static ManipurateUser this_ManipurateUser;
    [SerializeField] private Tilemap _tileMap;
    private Vector3 _pastPos;

    // インスペクターから指定
    [SerializeField] Animator anim;

    private TurnCharacter _turncharacter; 
    

    void Awake()
    {
        _userSprite = this.GetComponent<SpriteRenderer>();
        this_ManipurateUser = this.GetComponent<ManipurateUser>();
        _pastPos = GetBottomUserPos();
        _turncharacter = this.GetComponent<TurnCharacter>();
    }

    void FixedUpdate() 
    {
        if (GameMgr._gamemgr._gameState != GameState.Playing)
            return;
        Vector3 playerWorldPosBottom = GetBottomUserPos();

        float inputx = 0;
        float inputy = 0;
        (inputx, inputy) = InputFromUser();

        if (inputx != 0)
            _turncharacter.ChangeDirection( inputx > 0);

        // Vector3 playerWorldPosBottom_Next = playerWorldPosBottom + new Vector3(inputx * 0.1f, inputy * 0.1f);
        Vector3 playerWorldPos_Next = this.transform.position + new Vector3(inputx * 0.1f, inputy * 0.1f);

        this.transform.position = playerWorldPos_Next;
    }


    public static ManipurateUser Get()
    {
        return this_ManipurateUser;
    }

    private bool IsNextTileMovable(Vector3 clickPosition)
    {
        Vector3Int PlayerMapPosNext = _tileMap.WorldToCell(clickPosition); // ワールド座標からセル座標を取得
        bool isNextTileisVacant = _tileMap.GetColliderType(PlayerMapPosNext) == Tile.ColliderType.None;
        return isNextTileisVacant;
    }

    private Vector3 GetBottomUserPos()
    {
        float height = _userSprite.bounds.size.y;
        return this.transform.position + new Vector3(0, - height / 2f,0);
    }


    
    private (float, float) InputFromUser() {
        (float x,float y) = InputMoveDirectionFromKey();

        if( x != 0 || y != 0 ) {
            anim.SetBool ( "stop", false );
        } else {
            anim.SetBool ( "stop", true );
        } 

        return (x, y);
    }
    private (float, float) InputMoveDirectionFromKey() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
         y = 0;

        return (x, y);
    }


}
