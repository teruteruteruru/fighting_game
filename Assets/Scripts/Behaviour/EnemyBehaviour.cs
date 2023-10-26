using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region define
    /// <summary>
    /// ボスの状態
    /// </summary> 
    enum STATE_ENUM
	{
		/// <summary> 何もない状態 </summary>
		None,
		/// <summary> 動作する状態 </summary>
		Move,
		/// <summary> 死亡状態 </summary>
        Dead,
    }

    /// <summary>
    /// タスクの種類
    /// </summary>
    private enum TASK_ENUM
    {
        Jump,
        Wait,
    }
    #endregion

    #region serialize field
    [SerializeField, Header("指定した物理特性(重力等)の付与")]
    private Rigidbody2D _RigidBody = null;

    [SerializeField, Header("指定した画像を描画させる")]
    private SpriteRenderer _SpriteRenderer = null;

    [field: SerializeField, Header("Hp")]
    public int _Hp {get; private set;} = 5;

    [SerializeField, Header("移動速度")]
    float _MoveSpeed = 3.0f;
    // [SeriaizeField, Header("ジャンプ")]

    // 移動方向 true：左方向 false：右方向(仮)
    [SerializeField, Header("どっちに移動するか")] 
    bool _GoLeft = true;

    #endregion

    #region field
    /// <summary>
    /// ボスの状態
    /// </summary>
    private STATE_ENUM _State = STATE_ENUM.None;

    /// <summary>
    /// タスクリスト (要確認)
    /// </summary>
    #endregion

    #region property
    /// <summary> ボス死亡 </summary>
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
    #endregion

    #region Unity function
    // Updateメソッドが呼ばれる前のフレームで呼ばれる処理
    private void Start()
    {
        ChangeState(STATE_ENUM.Move);
    }

    // 毎フレーム呼ばれる処理
    private void Update()
    {
        UpdateState();
    }

    /// <summary>
    /// 衝突が起きた時に呼ばれる関数
    /// </summary>
    /// <param name="collision">衝突</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _GoLeft = !_GoLeft;
        }

        if(collision.gameObject.tag == "Bullet")
        {
            _Hp -= 1;
        }
    }
    #endregion

    #region public function
    #endregion

    #region private function

    /// <summary>
    /// 状態の変更
    /// この関数で変更の管理をするため、_Stateの直接の変更はしない！
    /// </summary>
    /// <param name="next">次の状態</param>
    private void ChangeState(STATE_ENUM next)
    {
        var prev = _State;
        _State = next;

        Log.Info(GetType(), "Change State {0} -> {1}", prev, next);

        // 変数が多い場合、条件分岐はswitchで書くと見やすい
        switch(_State)
        {
            case STATE_ENUM.None:
                {
                    // 描画させる
                    _SpriteRenderer.enabled = true;
                    ChangeState(STATE_ENUM.Move);
                }
                break;
            case STATE_ENUM.Move:
                {
                }
                break;
            case STATE_ENUM.Dead:
                {
                    // 描画をやめる
                    _SpriteRenderer.enabled = false;
                }
                break;
        }
    }

    /// <summary>
    /// 毎フレーム呼ばれる処理 (状態により変わる)
    /// </summary>
    private void UpdateState()
    {
        switch (_State)
        {
            case STATE_ENUM.None:
                {
                }
                break;
            case STATE_ENUM.Move:
                {
                    if (_RigidBody != null)
                        Move();
                }
                break;
            case STATE_ENUM.Dead:
                {
                }
                break;
        }
    }

    private void Move()
    {
        if (_SpriteRenderer.isVisible)
        {
            int xVector = 1;
            if(_GoLeft)
            {
                xVector = -1;
            }
            var v = _RigidBody.velocity;
            v.x = xVector * _MoveSpeed; // 指定した方向に進み続ける
            _RigidBody.velocity = v;

            if(this.gameObject.transform.position.x < -10f)
            {
                _GoLeft = false;
            }
            else if(this.gameObject.transform.position.x > 10f)
            {
                _GoLeft = true;
            }

            if (_Hp <= 0)
            {
                ChangeState(STATE_ENUM.Dead);
            }
        }
    }
    #endregion
}
