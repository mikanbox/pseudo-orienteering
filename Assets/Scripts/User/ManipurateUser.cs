using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ManipurateUser : MonoBehaviour
{
    private SpriteRenderer _userSprite;
    public static ManipurateUser _ManipurateUser;
    [SerializeField] private Tilemap _tileMap;
    private Vector3 _pastPos;

    // インスペクターから指定
    [SerializeField] Animator anim;

    private TurnCharacter _turncharacter; 

    private float moveamount = 0;

    // public bool lostPosition = false;
    private float lostPositionInterval = 0f;
    

    void Awake()
    {
        _userSprite = this.GetComponent<SpriteRenderer>();
        _ManipurateUser = this.GetComponent<ManipurateUser>();
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


        if (!User._user._islostPosition){
            (inputx, inputy) = InputFromUser();

            if (inputx != 0)
                _turncharacter.ChangeDirection( inputx > 0);

            // Vector3 playerWorldPosBottom_Next = playerWorldPosBottom + new Vector3(inputx * 0.1f, inputy * 0.1f);

            float normalized_inputx = 0;
            float normalized_inputy = 0;
            (normalized_inputx, normalized_inputy) = NormalizeValue(inputx,inputy);

            Vector3 playerWorldPos_Next = this.transform.position + new Vector3(normalized_inputx * 0.1f, normalized_inputy * 0.1f);
            this.transform.position = playerWorldPos_Next;
        }

        if (lostPositionInterval > 0) {
            lostPositionInterval -=Time.deltaTime;
            if (lostPositionInterval < 0) {
                lostPositionInterval = 0;
                User._user._islostPosition = false;
            }  
        }

        UpdateUserParameter(inputx * 0.1f);

        LostPositionCheck();
        // AddExtraMaptip();
    }

    private void UpdateUserParameter (float x) {
        float normalized_x = x;
        moveamount+= Mathf.Abs(normalized_x);

        float decreaseAmount = 0.1f;
        List<MapCode> OnMapcodes = GameMgr._gamemgr.GetNowTileMapCodes();
        for (int i = 0; i < OnMapcodes.Count; i++) {
            if (OnMapcodes[i] == MapCode.Forest)
                decreaseAmount *= 5.3f;
            if (OnMapcodes[i] == MapCode.WaterCource)
                decreaseAmount *= 10f;
        }

        if (moveamount > 0.1f) {
            moveamount-=0.1f;
            User._user.UpdateHP( decreaseAmount );
            User._user.UpdateUnTrackingPosition(0.1f);
        }

        if ( Mathf.Abs(x) == 0) {
            User._user.UpdateHP(-0.05f);
            User._user.UpdateUnTrackingPosition(-0.05f);
        }
        
    }

    private void LostPositionCheck() {
        if (User._user._parameter._trackingPosition > 30f + Random.Range(0f,10f)) {
            // lostPosition = true;
            User._user._islostPosition = true;
            lostPositionInterval = 2f + Random.Range(0f,2f);
        }
    }

    private void AddExtraMaptip() {
        if (User._user._parameter._trackingPosition > 30f) {
            GameMgr._gamemgr.AddExtraMapTip();
            User._user._parameter._trackingPosition -= 5f;
        }
    }

    private (float, float)  NormalizeValue (float x, float y) {
        float ux = (User._user.GetHPRatio() + 0.3f ) * x;
        float uy = (User._user.GetHPRatio() + 0.3f ) * y;

        return (ux,uy);
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

    public void MoveUserfromExternal (float x, float y) {
        if( x != 0 || y != 0 ) {
            anim.SetBool ( "stop", false );
        } else {
            anim.SetBool ( "stop", true );
        }

        Vector3 playerWorldPos_Next = this.transform.position + new Vector3(x * 0.1f, y * 0.1f);
        this.transform.position = playerWorldPos_Next;
    }

    public void StopUserAnimation () {
        anim.SetBool ( "stop", true );
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
