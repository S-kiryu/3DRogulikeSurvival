// スキルの実行時データ
public class SkillInstance
{
    public Skill skill;  // 元のScriptableObject（読み取り専用）
    public int remainingCount;  // ランタイム用のcount

    public SkillInstance(Skill skill)
    {
        this.skill = skill;
        this.remainingCount = skill.count;  // 初期化時のみコピー
    }
}