using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject character; // 角色游戏对象
    private ParticleSystem particles; // 粒子系统组件
    public int yOffset = 10, xOffset = 0, zOffset = 0;

    void Start()
    {
        particles = GetComponent<ParticleSystem>(); // 获取粒子系统组件
        character = GameObject.Find("Player");
    }

    void Update()
    {
        particles.transform.position = new Vector3(character.transform.position.x + xOffset,
                                                   character.transform.position.y + yOffset,
                                                   character.transform.position.z + zOffset); // 设置粒子系统位置
    }
}
