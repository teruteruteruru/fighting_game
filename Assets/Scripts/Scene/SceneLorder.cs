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
    
    #endregion

    #region property

    #endregion

    #region Unity function

   
   
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

    #endregion

    #region Task function

    #endregion Task function

    // #if UNITY_EDITOR 
    // #endif
    // �ň͂܂ꂽ�R�[�h�͎��s�o�C�i�����ɂ̓r���h����܂���B
    // �f�o�b�O�������� #if UNITY_EDITOR�ɏ����܂��傤

}
