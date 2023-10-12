using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region define
    /// <summary>
    /// プレイヤーの状態
    /// </summary>
    private enum STATE_ENUM
    {
        /// <summary> 何もない状態 </summary>
        None,
        /// <summary> 動作する状態 </summary>
        Move,
        /// <summary> 死亡状態 </summary>
        Dead,
    }
    #endregion

    #region serialize field
    //剛体(重力や速度等を制御するUnityのコンポーネント)
    [SerializeField]
    private Rigidbody2D _Rigidbody = null;

    //スプライト描画(スプライトの2D描画を制御するUnityのコンポーネント)
    [SerializeField]
    private SpriteRenderer _SpriteRenderer = null;

    /// <summary> 弾のプレハブ() </summary>
	[SerializeField]
    private GameObject _BulletPrefab = null;

    //移動速度
    [SerializeField]
    private float _MoveSpeed = 6.0f;

    // ジャンプの初速
    [SerializeField]
    private float _JumpSpeed = 17.0f;

    //弾の速度
    [SerializeField]
    private float _BulletSpeed = 10.0f;

    //体力
    [SerializeField]
    private int _Hp = 5;

    #endregion

    #region field
    /// <summary>
    /// プレイヤーの状態
    /// </summary>
    private STATE_ENUM _State = STATE_ENUM.None;
    #endregion

    #region property
    /// <summary> プレイヤー死亡 </summary>
    public bool IsDead
    {
        get
        {
            if (_State == STATE_ENUM.Dead)
                return true;
            else
                return false;
        }
    }

    public int Hp
    {
        get
        {
                return _Hp;
        }
    }

    #endregion

    #region Unity function
    /// <summary>
    /// オブジェクトが生成された直後、Unityから最初に１回呼ばれる処理
    /// </summary>
    private void Start()
    {
        ChangeState(STATE_ENUM.Move);
    }

    /// <summary>
    /// Unityから毎フレーム呼ばれる処理
    /// </summary>
    private void Update()
    {
        // 状態毎の処理を毎フレーム呼ぶ
        UpdateState();
    }

    /// <summary>
    /// コリジョンが当たった瞬間毎に１回だけ呼ばれる処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ボスにヒットした時の処理
        if (collision.gameObject.tag == "Enemy")
        {
            // ログを出す
            Log.Info(GetType(), "プレイヤーがボスにあたりました");

            // Dead状態に変更
            ChangeState(STATE_ENUM.Dead);
        }
    }
    #endregion

    #region public function
    #endregion

    #region private function
    /// <summary>
    /// 状態の変更
    /// ※メンバー変数 _Stateを変更する際はこの関数を呼ぶこと。
    /// 　この関数内以外で _Stateへの直接代入をしてはいけない。
    /// 　この関数を呼ぶことで以下の利点がある。
    /// 　1.ログが出る(デバッグ時に便利)
    /// 　2.状態変更時の処理が必ず呼ばれる
    /// </summary>
    /// <param name="next">次の状態</param>
    private void ChangeState(STATE_ENUM next)
    {
        // 以前の状態を保持
        var prev = _State;
        // 次の状態に変更する
        _State = next;

        // ログを出す
        Log.Info(GetType(), "ChangeState {0} -> {1}", prev, next);

        // 状態変更時に1回だけ呼ばれる処理を書く
        switch (_State)
        {
            case STATE_ENUM.None:
                // None変更時1回だけ呼ばれる処理
                {
                    _SpriteRenderer.enabled = true;
                }
                break;
            case STATE_ENUM.Move:
                // Move変更時1回だけ呼ばれる処理
                {
                }
                break;
            case STATE_ENUM.Dead:
                // Dead変更時1回だけ呼ばれる処理
                {
                    // 描画をOFFにする 
                    _SpriteRenderer.enabled = false;
                }
                break;
        }
    }

    /// <summary>
    /// 状態毎の毎フレーム呼ばれる処理
    /// </summary>
    private void UpdateState()
    {
        // 状態毎の毎フレーム呼ばれる処理
        switch (_State)
        {
            case STATE_ENUM.None:
                // None時に毎フレーム呼ばれる処理
                {
                }
                break;
            case STATE_ENUM.Move:
                // Move時に毎フレーム呼ばれる処理
                {
                    // 移動する処理
                    if (_Rigidbody != null)
                        Walk();

                    // ジャンプする処理
                    if (_Rigidbody != null)
                        Jump();

                    // 撃つ処理
                    if (_BulletPrefab != null)
                        Shot();

                    //志望判定
                    if(_Hp <= 0)
                    {
                        // ログを出す
                        Log.Info(GetType(), "プレイヤーの体力がなくなりました");

                        // Dead状態に変更
                        ChangeState(STATE_ENUM.Dead);
                    }
                       
                }
                break;
            case STATE_ENUM.Dead:
                // Dead時に毎フレーム呼ばれる処理
                {
                }
                break;
        }
    }

    /// <summary>
    /// 移動する処理
    /// </summary>
    private void Walk()
    {
      
        // ←→のキー入力を取得し、ローカル変数 dir を定義し代入する 
        float dir = 0f;

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            dir -= 1;
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            dir += 1;
        }



        //移動処理
        var v = _Rigidbody.velocity;
        v.x = _MoveSpeed * dir;
        _Rigidbody.velocity = v;

    }



    /// <summary>
    /// ジャンプする処理
    /// </summary>
    private void Jump()
    {
        // 地面に接地しているか確認
        var isGround = CheckGround();

        // 地面に接地していなければジャンプしない
        if (!isGround)
        {
            return;
        }

        // ↑キーを押したときの処理
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //ジャンプ処理
            var v = _Rigidbody.velocity;
            v.y = _JumpSpeed;
            _Rigidbody.velocity = v;
        }
    }

    /// <summary>
	/// 撃つ処理
	/// </summary>
	private void Shot()
    {
        // スペースキーを押した事を検知する
        if (Input.GetMouseButtonDown(0))
        {
            //自機からマウス位置へのベクトル
            Vector2 def = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
           
            //弾生成
            GameObject bulletObject = Instantiate(_BulletPrefab, transform.position, transform.rotation);

            //弾の初速設定
            bulletObject.GetComponent<Rigidbody2D>().velocity = def.normalized * _BulletSpeed;


        }
    }

    /// <summary>
    /// 地面判定用の処理
    /// プレイヤーから真下に直線（レイ）を引き、地面の当たり判定（コリジョン）と交差するかどうかを判定する
    /// </summary>
    /// <returns>地面と接地しているか</returns>
    private bool CheckGround()
    {
        // 地面のコリジョンの種類（レイヤー）は「Ground」という名前で定義されている場合
        // 判定するコリジョンレイヤーを表すローカル変数 layerMask を定義し、
        // "Ground"レイヤーのビットマスクをUnityのシステムから取得して代入する
        LayerMask layerMask = LayerMask.GetMask("Ground");

        // 下方向にレイを引き、"Ground"レイヤーのコリジョンとの交差判定をおこなう
        // 衝突情報を表すローカル変数 hitInfo を定義し、Physics2D.Raycast の 戻り値を取得して代入する
        // Physics2D.Raycast (レイの始点, レイの向き, 判定する範囲, 判定するコリジョンの種類)
        //  レイの始点 : プレイヤーの現在のポジション
        //  レイの向き : Y軸下向き(Vector2.down)
        //  判定する範囲 : 地面に設置していると判断できる長さ(1.0f程度)
        //  判定するコリジョンの種類 : layerMask
        var hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, layerMask);

        // レイの判定範囲内で地面のコリジョン(collider)と当たっていなければ false を返す
        // ・レイが当たると、 hitInfo.collider に値が入る
        // ・レイが当たらないと、 hitInfo.collider に値が入らない (nullとなる)
        if (hitInfo.collider == null)
            return false;

        // レイが地面と当たったので true を返す
        return true;
    }
    #endregion
}
