using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
	#region define

	private enum WHOSE
	{

		//プレイヤーのHP
		player,
		//敵のHP
		enemy


	}




	#endregion

	#region serialize field

	[SerializeField, Header("Playerの体力をもってくる")]
	private int player_HP = 5;

	[SerializeField, Header("ボスの体力をもってくる")]
	private int enemy_HP = 5;

	[SerializeField, Header("Playerの現在の体力")]
	private int player_HP_now = 0;

	[SerializeField, Header("ボスの現在の体力")]
	private int enemy_HP_now = 0;

	[SerializeField, Header("HPのゲームオブジェクト")]
	private GameObject hpObject = null;

	#endregion

	#region field
	//プレイヤーの残機の位置
	private Vector2 player_HP_pos = new Vector2(-8.5f, 4.5f);

	// 敵の残機の位置
	private Vector2 enemy_HP_pos = new Vector2(2.5f, 4.5f);

	//残機の位置の中心の間隔
	private Vector2 interval = new Vector2(1.0f, 0f);

	private GameObject player_obj;
	private GameObject enemy_obj;
	private PlayerBehaviour player_script;
	private EnemyBehaviour enemy_script;
	#endregion

	#region property

	#endregion

	#region Unity function

	// Updateメソッドが呼ばれる前のフレームで呼ばれる処理
	private void Start()
	{
		initilalization();
		player_HP_now = player_HP;

		enemy_HP_now = enemy_HP;

		Start_HPUI(WHOSE.player, player_HP_pos);
		Start_HPUI(WHOSE.enemy, enemy_HP_pos);
	}

	private void Update()
	{
		get_HP();
		update_HP();


	}

	#endregion

	#region public function
	#endregion

	#region private function

	private void Start_HPUI(WHOSE person, Vector2 position)
	{
		/// <summary>
		/// HPの描画の開始
		/// </summary>
        /// 
		int hp;
		string whose;


		if (person == WHOSE.player)
		{
			hp = player_HP;
			whose = "player_";

		}
		else
		{
			hp = enemy_HP;
			whose = "enemy_";

		}

		for (int i = 1; i <= hp; i++)
		{
			// HPを指定位置に生成
			var obj = Instantiate(hpObject, position + i * interval, Quaternion.identity);
			obj.name = whose + "HP" + i;
		}


	}
	private void get_HP()
	{
		Debug.Log("get hp");
		player_HP = player_script.Hp;
		enemy_HP = enemy_script._Hp;
		Debug.Log("player hp" + player_HP);
		Debug.Log("enemy hp" + enemy_HP);
	}

	private void update_HP()
	{
		while (player_HP_now > player_HP)
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

	#endregion
}
