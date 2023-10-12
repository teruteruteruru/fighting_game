using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region define
    /// <summary>
    /// �v���C���[�̏��
    /// </summary>
    private enum STATE_ENUM
    {
        /// <summary> �����Ȃ���� </summary>
        None,
        /// <summary> ���삷���� </summary>
        Move,
        /// <summary> ���S��� </summary>
        Dead,
    }
    #endregion

    #region serialize field
    //����(�d�͂⑬�x���𐧌䂷��Unity�̃R���|�[�l���g)
    [SerializeField]
    private Rigidbody2D _Rigidbody = null;

    //�X�v���C�g�`��(�X�v���C�g��2D�`��𐧌䂷��Unity�̃R���|�[�l���g)
    [SerializeField]
    private SpriteRenderer _SpriteRenderer = null;

    /// <summary> �e�̃v���n�u() </summary>
	[SerializeField]
    private GameObject _BulletPrefab = null;

    //�ړ����x
    [SerializeField]
    private float _MoveSpeed = 6.0f;

    // �W�����v�̏���
    [SerializeField]
    private float _JumpSpeed = 17.0f;

    //�e�̑��x
    [SerializeField]
    private float _BulletSpeed = 10.0f;

    //�̗�
    [SerializeField]
    private int _Hp = 5;

    #endregion

    #region field
    /// <summary>
    /// �v���C���[�̏��
    /// </summary>
    private STATE_ENUM _State = STATE_ENUM.None;
    #endregion

    #region property
    /// <summary> �v���C���[���S </summary>
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
    /// �I�u�W�F�N�g���������ꂽ����AUnity����ŏ��ɂP��Ă΂�鏈��
    /// </summary>
    private void Start()
    {
        ChangeState(STATE_ENUM.Move);
    }

    /// <summary>
    /// Unity���疈�t���[���Ă΂�鏈��
    /// </summary>
    private void Update()
    {
        // ��Ԗ��̏����𖈃t���[���Ă�
        UpdateState();
    }

    /// <summary>
    /// �R���W���������������u�Ԗ��ɂP�񂾂��Ă΂�鏈��
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �{�X�Ƀq�b�g�������̏���
        if (collision.gameObject.tag == "Enemy")
        {
            // ���O���o��
            Log.Info(GetType(), "�v���C���[���{�X�ɂ�����܂���");

            // Dead��ԂɕύX
            ChangeState(STATE_ENUM.Dead);
        }
    }
    #endregion

    #region public function
    #endregion

    #region private function
    /// <summary>
    /// ��Ԃ̕ύX
    /// �������o�[�ϐ� _State��ύX����ۂ͂��̊֐����ĂԂ��ƁB
    /// �@���̊֐����ȊO�� _State�ւ̒��ڑ�������Ă͂����Ȃ��B
    /// �@���̊֐����ĂԂ��Ƃňȉ��̗��_������B
    /// �@1.���O���o��(�f�o�b�O���ɕ֗�)
    /// �@2.��ԕύX���̏������K���Ă΂��
    /// </summary>
    /// <param name="next">���̏��</param>
    private void ChangeState(STATE_ENUM next)
    {
        // �ȑO�̏�Ԃ�ێ�
        var prev = _State;
        // ���̏�ԂɕύX����
        _State = next;

        // ���O���o��
        Log.Info(GetType(), "ChangeState {0} -> {1}", prev, next);

        // ��ԕύX����1�񂾂��Ă΂�鏈��������
        switch (_State)
        {
            case STATE_ENUM.None:
                // None�ύX��1�񂾂��Ă΂�鏈��
                {
                    _SpriteRenderer.enabled = true;
                }
                break;
            case STATE_ENUM.Move:
                // Move�ύX��1�񂾂��Ă΂�鏈��
                {
                }
                break;
            case STATE_ENUM.Dead:
                // Dead�ύX��1�񂾂��Ă΂�鏈��
                {
                    // �`���OFF�ɂ��� 
                    _SpriteRenderer.enabled = false;
                }
                break;
        }
    }

    /// <summary>
    /// ��Ԗ��̖��t���[���Ă΂�鏈��
    /// </summary>
    private void UpdateState()
    {
        // ��Ԗ��̖��t���[���Ă΂�鏈��
        switch (_State)
        {
            case STATE_ENUM.None:
                // None���ɖ��t���[���Ă΂�鏈��
                {
                }
                break;
            case STATE_ENUM.Move:
                // Move���ɖ��t���[���Ă΂�鏈��
                {
                    // �ړ����鏈��
                    if (_Rigidbody != null)
                        Walk();

                    // �W�����v���鏈��
                    if (_Rigidbody != null)
                        Jump();

                    // ������
                    if (_BulletPrefab != null)
                        Shot();

                    //�u�]����
                    if(_Hp <= 0)
                    {
                        // ���O���o��
                        Log.Info(GetType(), "�v���C���[�̗̑͂��Ȃ��Ȃ�܂���");

                        // Dead��ԂɕύX
                        ChangeState(STATE_ENUM.Dead);
                    }
                       
                }
                break;
            case STATE_ENUM.Dead:
                // Dead���ɖ��t���[���Ă΂�鏈��
                {
                }
                break;
        }
    }

    /// <summary>
    /// �ړ����鏈��
    /// </summary>
    private void Walk()
    {
      
        // �����̃L�[���͂��擾���A���[�J���ϐ� dir ���`��������� 
        float dir = 0f;

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            dir -= 1;
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            dir += 1;
        }



        //�ړ�����
        var v = _Rigidbody.velocity;
        v.x = _MoveSpeed * dir;
        _Rigidbody.velocity = v;

    }



    /// <summary>
    /// �W�����v���鏈��
    /// </summary>
    private void Jump()
    {
        // �n�ʂɐڒn���Ă��邩�m�F
        var isGround = CheckGround();

        // �n�ʂɐڒn���Ă��Ȃ���΃W�����v���Ȃ�
        if (!isGround)
        {
            return;
        }

        // ���L�[���������Ƃ��̏���
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //�W�����v����
            var v = _Rigidbody.velocity;
            v.y = _JumpSpeed;
            _Rigidbody.velocity = v;
        }
    }

    /// <summary>
	/// ������
	/// </summary>
	private void Shot()
    {
        // �X�y�[�X�L�[���������������m����
        if (Input.GetMouseButtonDown(0))
        {
            //���@����}�E�X�ʒu�ւ̃x�N�g��
            Vector2 def = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
           
            //�e����
            GameObject bulletObject = Instantiate(_BulletPrefab, transform.position, transform.rotation);

            //�e�̏����ݒ�
            bulletObject.GetComponent<Rigidbody2D>().velocity = def.normalized * _BulletSpeed;


        }
    }

    /// <summary>
    /// �n�ʔ���p�̏���
    /// �v���C���[����^���ɒ����i���C�j�������A�n�ʂ̓����蔻��i�R���W�����j�ƌ������邩�ǂ����𔻒肷��
    /// </summary>
    /// <returns>�n�ʂƐڒn���Ă��邩</returns>
    private bool CheckGround()
    {
        // �n�ʂ̃R���W�����̎�ށi���C���[�j�́uGround�v�Ƃ������O�Œ�`����Ă���ꍇ
        // ���肷��R���W�������C���[��\�����[�J���ϐ� layerMask ���`���A
        // "Ground"���C���[�̃r�b�g�}�X�N��Unity�̃V�X�e������擾���đ������
        LayerMask layerMask = LayerMask.GetMask("Ground");

        // �������Ƀ��C�������A"Ground"���C���[�̃R���W�����Ƃ̌�������������Ȃ�
        // �Փˏ���\�����[�J���ϐ� hitInfo ���`���APhysics2D.Raycast �� �߂�l���擾���đ������
        // Physics2D.Raycast (���C�̎n�_, ���C�̌���, ���肷��͈�, ���肷��R���W�����̎��)
        //  ���C�̎n�_ : �v���C���[�̌��݂̃|�W�V����
        //  ���C�̌��� : Y��������(Vector2.down)
        //  ���肷��͈� : �n�ʂɐݒu���Ă���Ɣ��f�ł��钷��(1.0f���x)
        //  ���肷��R���W�����̎�� : layerMask
        var hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, layerMask);

        // ���C�̔���͈͓��Œn�ʂ̃R���W����(collider)�Ɠ������Ă��Ȃ���� false ��Ԃ�
        // �E���C��������ƁA hitInfo.collider �ɒl������
        // �E���C��������Ȃ��ƁA hitInfo.collider �ɒl������Ȃ� (null�ƂȂ�)
        if (hitInfo.collider == null)
            return false;

        // ���C���n�ʂƓ��������̂� true ��Ԃ�
        return true;
    }
    #endregion
}
