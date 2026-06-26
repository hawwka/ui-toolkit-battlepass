using UnityEngine;

namespace Project.UI.BattlePass.Providers
{
    public interface IRewardIconProvider
    {
        Sprite GetIcon(string assetKey);
    }
}
