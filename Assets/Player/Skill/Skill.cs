using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Game/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private int id;
    public int ID => id;

    //Skillの名前
    public string skillName;
    //Skillの説明
    public string skillDescription;
    //Skillのアイコン
    public Sprite icon;
    //クールダウン
    public float cooldown;
    //スキルの効果プレハブ
    public GameObject effectPrefab;

    public SkillEffect skillEffect;

    public List<Skill> upgradeSkills;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // IDがまだ設定されていなければGUIDから自動生成
        if (id == 0)
        {
            id = name.GetHashCode();
        }
    }
#endif
}
