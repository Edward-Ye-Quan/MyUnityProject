using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//�����ˣ�ҹȪ
//����˵������һ��������������Ϸ�Ļ�������
//������Ƶ�������ڻ�����Ϸȫ��ʹ�ã���˿���ͳһ����
//***************************************** 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private AudioSource audioSource;
    // [SerializeField] �� Unity �е�һ�����ԣ�
    // ���ڽ�˽���ֶα�¶�� Unity �༭����
    // ʹ������ڱ༭���н��и�ֵ�͵�����
    // �����轫�ֶεķ��ʼ�������Ϊ public��
    // �������ڱ��ַ�װ�ԣ�ͬʱ�ṩ�༭��������ԡ�
    // �򵥽���
    // [SerializeField] �� private ���ֶ�
    // ֻ���� ���ڲ����и�ֵ
    // ������ Unity �༭����ͨ���϶�������и�ֵ��
    // ������ ������򷽷���ֱ�ӷ��ʺ͸�ֵ��

    //  Ϊ��ȷ�� GameManager ��ĵ���ģʽ��������Ϸ������������Ч��
    //  ��Ҫ�������¼��㣺
    //1.ȷ�� Instance ֻ����ֵһ�Ρ�
    //2.��ֹ�ڳ����л�ʱ���� GameManager ʵ����
    //3.ȷ��ֻ��һ�� GameManager ʵ�����ڡ�
    private void Awake()
    {
        if (Instance == null) // 1
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 2

            // ȷ�� audioSource ����ȷ��ʼ��
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 3
        }
    }
    //  ���У�
    //  һ��gameObject
    //  �����C++����ȣ�gameObject ������һ��ָ������ָ��
    //  ͨ�� gameObject�����Է��ʺͲ�������Ϸ����ĸ�����������ԡ�
    //  ����Awake����
    //  ���ڽű�ʵ��������ʱ�Զ����á�
    //  ���彲�����������������
    //1.��Ϸ���󱻼���ʱ���������ýű�����Ϸ���󱻼���ʱ��Awake �����ᱻ���á�
    //2.��������ʱ������������ʱ�����м������Ϸ�����ϵ� Awake �����ᱻ���á�

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="clip">������Դ</param>
    public void PlayMusic(AudioClip clip)
    {
        // ����쳣�����
        if (clip == null){
            Debug.LogWarning("GameManager::PlayMusic::AudioClip == null");
            return;
        }
        if (audioSource == null){
            Debug.LogError("GameManager::PlayMusic::AudioSource == null");
            return;
        }
        if (audioSource.isPlaying) audioSource.Stop(); // ֹͣ��ǰ���ŵ�����

        audioSource.clip = clip; // ��audioSource�е���Ƶ��Ϊ�÷�������������Ƶclip
        audioSource.Play(); // ������Ƶ
    }
    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="clip">��Ч��Դ</param>
    public void PlaySound(AudioClip clip)
    {
        // ����쳣�����
        if (clip == null)
        {
            Debug.LogWarning("GameManager::PlayMusic::AudioClip == null");
            return;
        }
        if (audioSource == null)
        {
            Debug.LogError("GameManager::PlayMusic::AudioSource == null");
            return;
        }

        audioSource.PlayOneShot(clip); // ����һ��
    }
}
