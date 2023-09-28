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
        player_obj = GameObject.Find("Player"); //Player���Ă����I�u�W�F�N�g��T��
        player_script = obj.GetComponent<PlayerBehaviour>(); //�t���Ă���X�N���v�g���擾

        enemy_obj = GameObject.Find("Enemy"); //Player���Ă����I�u�W�F�N�g��T��
        enemy_script = obj.GetComponent<EnemyBehaviour>(); //�t���Ă���X�N���v�g���擾
    }

    public void SceneLorderStart()
    {
        /// <summary>
        /// �X�^�[�g�V�[���̌Ăяo��
        /// </summary>
        
    #if UNITY_EDITOR
	    Debug.Log ("LordStartScene");
    #endif

        SceneManager.LoadScene("Scenes/StartScene");
    }
    public void SceneLorderGame()
    {
        /// <summary>
        /// �Q�[���V�[���̌Ăяo��
        /// </summary>


    #if UNITY_EDITOR
	    Debug.Log ("LordGameScene");
    #endif
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void SceneLorderResult()
    {
        /// <summary>
        /// ���U���g�V�[���̌Ăяo��
        /// </summary>

    #if UNITY_EDITOR
	          Debug.Log ("LordResultScene");
    #endif

        SceneManager.LoadScene("Scenes/ResultScene");
    }

    #endregion

    #region public function

    #endregion

    #region private function

    private void Enemy_Dead()
    {
        /// <summary>
        /// �{�X�����񂾎��̏���
        /// </summary>

        if (enemy_is_dead == true)
        {

            SceneLorderResult();

        }

    }

    private void player_Dead()
    {
        /// <summary>
        /// �{�X�����񂾎��̏���
        /// </summary>

        if (player_is_dead == true)
        {

            SceneLorderResult();

        }

    }

    private void get_state()
    {
        
    }

    #endregion

    #region Task function

    #endregion Task function

    // #if UNITY_EDITOR 
    // #endif
    // �ň͂܂ꂽ�R�[�h�͎��s�o�C�i�����ɂ̓r���h����܂���B
    // �f�o�b�O�������� #if UNITY_EDITOR�ɏ����܂��傤

}
