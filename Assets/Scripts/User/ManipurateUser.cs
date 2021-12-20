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

            float normalized_inputx = 0;
            float normalized_inputy = 0;
            (normalized_inputx, normalized_inputy) = NormalizeValueWithHP(inputx,inputy);

            Vector3 playerWorldPos_Next = this.transform.position + new Vector3(normalized_inputx,  normalized_inputy)* 0.1f;
            this.transform.position = playerWorldPos_Next;
        }

        if (lostPositionInterval > 0) 
            lostPositionInterval -=Time.deltaTime;
        if (lostPositionInterval < 0) {
            lostPositionInterval = 0;
            User._user._islostPosition = false;
        }  
        

        UpdateUserParameter(inputx * 0.1f);

        LostPositionCheck();
        // AddExtraMaptip();
    }


// 移動に伴うユーザーパラメータ更新
    private void UpdateUserParameter (float x) {
        float normalized_x = x;
        moveamount+= Mathf.Abs(normalized_x);
        GameMgr._gamemgr._totalmovedistance+=moveamount;


        float decreaseAmount = 0.05f;


        decreaseAmount /= (1.0f / Mathf.Log10(User._user._parameter._stamina) );

        if (moveamount > 0.1f) {
            moveamount-=0.1f;
            User._user.UpdateHP( decreaseAmount );
            User._user.UpdateUnTrackingPosition(0.3f / Mathf.Log10(User._user._parameter._intelligence) );

            if (User._user._parameter._hp / User._user._parameter._maxhp < 0.2)
                User._user._userexp._guts+=2;
            if (User._user._parameter._hp / User._user._parameter._maxhp > 0.7)
                User._user._userexp._speed++;
            if (User._user._parameter._hp / User._user._parameter._maxhp < 0.7)
                User._user._userexp._stamina++;
        }

        if ( Mathf.Abs(x) == 0) {
            User._user.UpdateHP(-0.02f);
            User._user.UpdateUnTrackingPosition(-0.03f);
            if (User._user._parameter._trackingPosition <60 && User._user._parameter._trackingPosition > 10)
                User._user._userexp._intelligence++;
        }
    }




    private void LostPositionCheck() {
        if (User._user._parameter._trackingPosition > 100) {
            // lostPosition = true;
            User._user._islostPosition = true;
            lostPositionInterval = 2f + Random.Range(0f,2f);
        }
    }

    private void AddExtraMaptip() {
        if (User._user._parameter._trackingPosition > 30f) {
            // GameMgr._gamemgr.AddExtraMapTip();
            User._user._parameter._trackingPosition -= 5f;
        }
    }

    private (float, float)  NormalizeValueWithHP (float x, float y) {
        float attenuation = (1.0f - (1.0f - User._user.GetHPRatio()) / Mathf.Log10(User._user._parameter._guts) );
        float magnification = Mathf.Log10(User._user._parameter._speed) * 0.8f;
        List<MapCode> OnMapcodes = GameMgr._gamemgr.GetNowTileMapCodes();
        for (int i = 0; i < OnMapcodes.Count; i++) {
            if (OnMapcodes[i] == MapCode.Forest)
                magnification *= 0.3f;
            if (OnMapcodes[i] == MapCode.WaterCource)
                magnification *= 0.2f;
            if (OnMapcodes[i] == MapCode.Open)
                magnification *= 0.8f;   
        }

        float ux = ( attenuation + 0.1f ) * x * magnification;
        float uy = ( attenuation + 0.1f ) * y * magnification;

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
        // (float x,float y) = InputMoveDirectionFromKey();
        (float x,float y) = InputMoveDirectionFromMOvileDevice();

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

    private (float, float) InputMoveDirectionFromMOvileDevice() {
        float x = 0.5f;
        if (Input.GetMouseButton(0)) {
            x = 0;
        }
        float y = 0;

        return (x, y);
    }


}
