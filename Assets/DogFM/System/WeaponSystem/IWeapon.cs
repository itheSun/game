using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public abstract class IWeapon
{
    protected int id;
    protected string name;

    protected int atk = 0;
    protected float atkRange = 0.0f;

    /// <summary>
    /// ����ģ��
    /// </summary>
    protected GameObject prefGO;
    /// <summary>
    /// ����������
    /// </summary>
    protected ICharacter character;

    protected ParticleSystem particleSystem;
    protected AudioSource audioSource;
    protected LineRenderer lineRenderer;
    protected Light light;

    protected void PlayShootEffect()
    {

    }

    protected void PlaySoundEffect()
    {

    }

    public virtual void Fire()
    {

    }
}
