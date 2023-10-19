using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneLorder : MonoBehaviour
{
    #region define

    #endregion

    #region serialize field

    #endregion

    #region field

    private GameObject player_obj;
    private GameObject enemy_obj;
    private bool player_is_dead;
    private bool enemy_is_dead;
    private PlayerBehaviour player_script;
    private EnemyBehaviour enemy_script;

    #endregion

    #region property

    #endregion

    #region Unity function

    private void Start()
    {
        initilalization();
    }


    private void Update()
    {
        get_state();
        enemy_Dead();
        player_Dead();
    }



    #endregion

    #region public function

    public void SceneLorderStart()
    {
        /// <summary>
        /// スタートシーンの呼び出し
        /// </summary>

#if UNITY_EDITOR
	    Debug.Log ("LordStartScene");
#endif

        SceneManager.LoadScene("Scenes/StartScene");
    }
    public void SceneLorderGame()
    {
        /// <summary>
        /// ゲームシーンの呼び出し
        /// </summary>


#if UNITY_EDITOR
	    Debug.Log ("LordGameScene");
#endif
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void SceneLorderResult()
    {
        /// <summary>
        /// リザルトシーンの呼び出し
        /// </summary>

#if UNITY_EDITOR
	          Debug.Log ("LordResultScene");
#endif

        SceneManager.LoadScene("Scenes/ResultScene");
    }


    #endregion

    #region private function

    private void initilalization()
    {
        /// <summary>
        /// 初期化してスクリプトを読み込む
        /// </summary>

        player_obj = GameObject.Find("Player"); //Playerっていうオブジェクトを探す
        player_script = player_obj.GetComponent<PlayerBehaviour>(); //付いているスクリプトを取得

        enemy_obj = GameObject.Find("Enemy"); //Playerっていうオブジェクトを探す
        enemy_script = enemy_obj.GetComponent<EnemyBehaviour>(); //付いているスクリプトを取得

    }

    private void enemy_Dead()
    {
        /// <summary>
        /// ボスが死んだ時の処理
        /// </summary>

        if (enemy_is_dead == true)
        {

            SceneLorderResult();

        }

    }

    private void player_Dead()
    {
        /// <summary>
        /// ボスが死んだ時の処理
        /// </summary>

        if (player_is_dead == true)
        {

            SceneLorderResult();

        }

    }

    private void get_state()
    {
        /// <summary>
        /// プレイヤーとボスのステータス更新
        /// </summary>
        player_is_dead = player_script.IsDead;
        enemy_is_dead = enemy_script.IsDead;

    }

    #endregion

    #region Task function

    #endregion Task function

    // #if UNITY_EDITOR 
    // #endif
    // で囲まれたコードは実行バイナリ環境にはビルドされません。
    // デバッグ処理等は #if UNITY_EDITORに書きましょう

}
