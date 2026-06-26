using System.Collections.Generic;
using Project.UI.BattlePass.Config;
using UnityEngine;

namespace Project.UI.BattlePass.Providers
{
    public sealed class SerializedRewardIconProvider : IRewardIconProvider
    {
        private readonly Dictionary<string, Sprite> _iconsByKey = new();

        public SerializedRewardIconProvider(IEnumerable<RewardIconEntry> entries)
        {
            if (entries == null)
                return;

            foreach (var entry in entries)
            {
                if (entry == null || string.IsNullOrWhiteSpace(entry.AssetKey) || entry.Sprite == null)
                    continue;

                _iconsByKey[entry.AssetKey] = entry.Sprite;
            }
        }

        public Sprite GetIcon(string assetKey)
        {
            if (string.IsNullOrWhiteSpace(assetKey))
                return null;

            return _iconsByKey.TryGetValue(assetKey, out var sprite) ? sprite : null;
        }
    }
}
