using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
	#region define

	private enum WHOSE
    {

		//�v���C���[��HP
		player,
		//�G��HP
		enemy


    }


	//�v���C���[�̎c�@�̈ʒu
	private Vector2 player_HP_pos = new Vector2(-8.5f, 4.5f);
	
	// �G�̎c�@�̈ʒu
	private Vector2 enemy_HP_pos = new Vector2(2.5f, 4.5f);

	//�c�@�̈ʒu�̒��S�̊Ԋu
	private Vector2 interval = new Vector2(1.0f, 0f);

	#endregion

	#region serialize field

	[SerializeField, Header("Player�̗̑͂������Ă���")]
	private int player_HP = 5;

	[SerializeField, Header("�{�X�̗̑͂������Ă���")]
	private int enemy_HP = 5;

	[SerializeField, Header("Player�̌��݂̗̑�")]
	private int player_HP_now = 0;

	[SerializeField, Header("�{�X�̌��݂̗̑�")]
	private int enemy_HP_now = 0;

	[SerializeField, Header("HP�̃Q�[���I�u�W�F�N�g")]
	private GameObject hpObject = null;

	#endregion

	#region field

	#endregion

	#region property

	#endregion

	#region Unity function

	// Update���\�b�h���Ă΂��O�̃t���[���ŌĂ΂�鏈��
	private void Start()
	{
		player_HP_now = player_HP;

		enemy_HP_now = enemy_HP;

		Start_HPUI(WHOSE.player, player_HP_pos);
		Start_HPUI(WHOSE.enemy, enemy_HP_pos);
	}

	private void Update()
    {

		update_HP();


	}

	#endregion

	#region public function
	#endregion

	#region private function

	private void Start_HPUI(WHOSE person,Vector2 position) {

		int hp;
		string whose;


		if(person == WHOSE.player)
        {
			hp = player_HP;
			whose = "player_";
			
        }
        else
        {
			hp = enemy_HP;
			whose = "enemy_";

		}

		for(int i = 1; i<= hp; i++)
        {
			// HP���w��ʒu�ɐ���
			var obj = Instantiate(hpObject, position + i*interval, Quaternion.identity);
			obj.name = whose + "HP" + i;
		}
		

	}

	private void update_HP()
    {
		while(player_HP_now > player_HP)
        {
			GameObject obj = GameObject.Find("player_HP" + player_HP_now);
			Destroy(obj);
			player_HP_now--;
		}

		while (enemy_HP_now > enemy_HP)
		{

			GameObject obj = GameObject.Find("enemy_HP" + enemy_HP_now);
			Destroy(obj);
			enemy_HP_now--;
		}

	}

	#endregion
}
