using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Constants
{
    #region Game Constants

    public const float PORTAL_TIMER_AMOUNT = 1f;

    #endregion

    #region Player Constants

    public const float PLAYER_HEALTH_AMOUNT = 100f;
    public const float PLAYER_RESPAWN_HEALTH_AMOUNT = 75f;
    public const float PLAYER_MAX_HORIZONTAL_SPEED = 2.5f;
    public const float PLAYER_HORIZONTAL_ACCELERATION = 5f;
    public const float PLAYER_JUMP_FORCE = 5f;
    public const float PLAYER_BASIC_ATTACK_BULLET_SPEED = 5f;
    public const float PLAYER_BASIC_ATTACK_BULLET_LIFETIME = 0.5f;

    #endregion

    #region Enemy Constants

    public const float BASIC_ENEMY_MAX_HORIZONTAL_SPEED = 1f;
    public const float BASIC_ENEMY_HORIZONTAL_ACCELERATION = 2f;
    public const float BASIC_ENEMY_DETECTION_DISTANCE = 3f;
    public const float BASIC_ENEMY_MELEE_HEALTH = 5f;
    public const float BASIC_ENEMY_MELEE_DAMAGE_TO_PLAYER = 10f;
    public const float BASIC_ENEMY_RANGED_HEALTH = 3f;
    public const float BASIC_ENEMY_RANGED_FIRING_DISTANCE = 2f;
    public const float BASIC_ENEMY_RANGED_FIRING_TIMER = 1f;
    public const float BASIC_ENEMY_RANGED_DAMAGE_TO_PLAYER = 10f;
    public const float BASIC_ENEMY_PROJECTILE_SPEED = 2f;
    public const float BASIC_ENEMY_PROJECTILE_LIFETIME = 1f;
    public const float BASIC_ENEMY_PROJECTILE_DAMAGE_TO_PLAYER = 10f;

    #endregion

    #region Items

    public const float ITEM_HEALTH_POTION_RESTORATION = 50f;
    public const float SHIELD_DURATION = 10f;

    #endregion
}