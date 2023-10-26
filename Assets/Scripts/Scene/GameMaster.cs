using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    #region define

    #endregion

    #region serialize field

    #endregion

    #region field

    private GameObject player_obj;
    private GameObject enemy_obj;
    private GameObject scene_obj;
    private bool player_is_dead;
    private bool enemy_is_dead;
    private PlayerBehaviour player_script;
    private EnemyBehaviour enemy_script;
    private SceneLorder scene_script;

    #endregion

    #region property

    #endregion

    #region Unity function

    // Update���\�b�h���Ă΂��O�̃t���[���ŌĂ΂�鏈��
    private void Start()
    {
        Initilalization();
    }

    // ���t���[���Ă΂�鏈��
    private void Update()
    {
        Get_state();
        Enemy_Dead();
        Player_Dead();
    }



    #endregion

    #region public function

    #endregion

    #region private function

    private void Initilalization()
    {
        /// <summary>
        /// ���������ăX�N���v�g��ǂݍ���
        /// </summary>

        player_obj = GameObject.Find("Player"); //Player���Ă����I�u�W�F�N�g��T��
        player_script = player_obj.GetComponent<PlayerBehaviour>(); //�t���Ă���X�N���v�g���擾

        enemy_obj = GameObject.Find("Enemy"); //Player���Ă����I�u�W�F�N�g��T��
        enemy_script = enemy_obj.GetComponent<EnemyBehaviour>(); //�t���Ă���X�N���v�g���擾

        scene_obj = GameObject.Find("SceneLorder");
        scene_script = scene_obj.GetComponent<SceneLorder>();

    }

    private void Enemy_Dead()
    {
        /// <summary>
        /// �{�X�����񂾎��̏���
        /// </summary>

        if (enemy_is_dead == true)
        {

            scene_script.SceneLorderResult();

        }

    }

    private void Player_Dead()
    {
        /// <summary>
        /// �{�X�����񂾎��̏���
        /// </summary>

        if (player_is_dead == true)
        {

            scene_script.SceneLorderResult();

        }

    }

    private void Get_state()
    {
        /// <summary>
        /// �v���C���[�ƃ{�X�̃X�e�[�^�X�X�V
        /// </summary>
        player_is_dead = player_script.IsDead;
        enemy_is_dead = enemy_script.IsDead;

    }

    #endregion

    #region Task function

    #endregion Task function

    // #if UNITY_EDITOR 
    // #endif
    // �ň͂܂ꂽ�R�[�h�͎��s�o�C�i�����ɂ̓r���h����܂���B
    // �f�o�b�O�������� #if UNITY_EDITOR�ɏ����܂��傤
}
