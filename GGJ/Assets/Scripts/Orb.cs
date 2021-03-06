﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [Header("General")]
    public OrbData data;

    [Header("Mechanics")]
    public Ability initialAbility = Ability.NULL;
    Ability mCrrtAbility;

    public Ability crrtAbility
    {
        get
        {
            return mCrrtAbility;
        }
        set
        {
            mCrrtAbility = value;
            spriteRenderer.sprite = data.orbSprites[(int)mCrrtAbility];
        }
    }
    bool mIsOrbTouched = false;
    public bool isOrbTouched
    {
        get
        {
            return mIsOrbTouched;
        }
        set
        {
            if (!mIsOrbTouched && value)
            {
                doorManager.notifyOrbTouched();
            }
            mIsOrbTouched = value;
        }
    }

    [Header("Components")]
    public SpriteRenderer spriteRenderer;
    public DoorManager doorManager;

    private void Start()
    {
        doorManager.registerOrb();
        crrtAbility = initialAbility;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            isOrbTouched = true;
            crrtAbility = player.SwapAbility(crrtAbility);
        }
    }
}
