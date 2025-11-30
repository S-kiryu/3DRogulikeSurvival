using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Game/Skill")]
public class Skill : ScriptableObject
{
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
    public void Activate(Vector3 position)
    {
        if (effectPrefab != null)
        {
            GameObject.Instantiate(effectPrefab, position, Quaternion.identity);
        }
    }
}
