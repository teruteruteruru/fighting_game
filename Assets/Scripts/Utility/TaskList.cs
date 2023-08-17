using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;



/// </summary>
/// <typeparam name="T"></typeparam>
public class TaskList<T>
{
    #region define
    private class Task
    {
        public T TaskType;
        public Action Enter { get; set; }
        public Func<bool> Update { get; set; }
        public Action Exit { get; set; }

        public Task(T t, Action enter, Func<bool> update, Action exit)
        {
            TaskType = t;
            Enter = enter;
            Update = update ?? delegate { return true; };
            Exit = exit;
        }
    }
    #endregion

    #region field
    /// <summary> ��`���ꂽ�^�X�N </summary>
    Dictionary<T, Task> _DefineTaskDictionary = new Dictionary<T, Task>();
    /// <summary> ���ݐς܂�Ă���^�X�N </summary>
    List<Task> _CurrentTaskList = new List<Task>();
    /// <summary> ���ݓ��삵�Ă���^�X�N </summary>
    Task _CurrentTask = null;
    /// <summary> ���݂�Index�ԍ� </summary>
    int _CurrentIndex = 0;
    #endregion

    #region property
    /// <summary>
    /// �ǉ����ꂽ�^�X�N�����ׂďI�����Ă��邩
    /// </summary>
    public bool IsEnd
    {
        get { return _CurrentTaskList.Count <= _CurrentIndex; }
    }

    /// <summary>
    ///  �^�X�N�������Ă��邩
    /// </summary>
    public bool IsMoveTask
    {
        get { return _CurrentTask != null; }
    }

    /// <summary>
    /// ���݂̃^�X�N�^�C�v
    /// </summary>
    public T CurrentTaskType
    {
        get
        {
            if (_CurrentTask == null)
                return default(T);
            return _CurrentTask.TaskType;
        }
    }

    /// <summary>
    /// �ǉ�����Ă���^�X�N�̃��X�g
    /// </summary>
    public List<T> CurrentTaskTypeList
    {
        get
        {
            return _CurrentTaskList.Select(x => x.TaskType).ToList();
        }
    }

    /// <summary>
    /// ���݂̃C���f�b�N�X
    /// </summary>
    public int CurrentIndex
    {
        get { return _CurrentIndex; }
    }
    #endregion

    #region public function
    /// <summary>
    /// ���t���[���Ă΂�鏈��
    /// (Behaviour��Update�ŌĂ΂��z��)
    /// </summary>
    public void UpdateTask()
    {
        // �^�X�N���Ȃ���Ή������Ȃ�
        if (IsEnd)
        {
            return;
        }

        // ���݂̃^�X�N���Ȃ���΁A�^�X�N���擾����
        if (_CurrentTask == null)
        {
            _CurrentTask = _CurrentTaskList[_CurrentIndex];
            // Enter���Ă�
            _CurrentTask.Enter?.Invoke();
        }

        // �^�X�N��Update���Ă�
        var isEndOneTask = _CurrentTask.Update();

        // �^�X�N���I�����Ă���Ύ��̏������Ă�
        if (isEndOneTask)
        {
            // ���݂̃^�X�N��Exit���Ă�
            _CurrentTask?.Exit();

            // Index�ǉ�
            _CurrentIndex++;

            // �^�X�N���Ȃ���΃N���A����
            if (IsEnd)
            {
                _CurrentIndex = 0;
                _CurrentTask = null;
                _CurrentTaskList.Clear();
                return;
            }

            // ���̃^�X�N���擾����
            _CurrentTask = _CurrentTaskList[_CurrentIndex];
            // ���̃^�X�N��Enter���Ă�
            _CurrentTask?.Enter();
        }
    }

    /// <summary>
    /// �^�X�N�̒�`
    /// </summary>
    /// <param name="t"></param>
    /// <param name="enter"></param>
    /// <param name="update"></param>
    /// <param name="exit"></param>
    public void DefineTask(T t, Action enter, Func<bool> update, Action exit)
    {
        var task = new Task(t, enter, update, exit);
        var exist = _DefineTaskDictionary.ContainsKey(t);
        if (exist)
        {
#if UNITY_EDITOR
            Log.Error(GetType(), "{0}�͊��ɒǉ�����Ă��܂��B(�o�^����܂���ł���).", t);
#endif
            return;
        }
        _DefineTaskDictionary.Add(t, task);
    }

    /// <summary>
    /// �^�X�N�̓o�^
    /// </summary>
    /// <param name="t"></param>
    public void AddTask(T t)
    {
        Task task = null;
        var exist = _DefineTaskDictionary.TryGetValue(t, out task);
        if (exist == false)
        {
#if UNITY_EDITOR
            Log.Error(GetType(), "{0}�̃^�X�N���o�^����Ă��Ȃ��̂Œǉ��ł��܂���.", t);
#endif
            return;
        }
        _CurrentTaskList.Add(task);
    }

    /// <summary>
    /// �����I��
    /// </summary>
    public void ForceStop()
    {
        if (_CurrentTask != null)
        {
            _CurrentTask.Exit();
        }
        _CurrentTask = null;
        _CurrentTaskList.Clear();
        _CurrentIndex = 0;
    }
    #endregion
}

