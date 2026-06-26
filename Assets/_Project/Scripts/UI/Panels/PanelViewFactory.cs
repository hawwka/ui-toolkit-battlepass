using System;
using System.Collections.Generic;
using Project.UI.BattlePass;
using Project.UI.BattlePass.Config;
using Project.UI.Navigation;

namespace Project.UI.Panels
{
    public sealed class PanelViewFactory
    {
        private readonly Dictionary<string, PanelConfig> _configsById;

        public PanelViewFactory(IEnumerable<PanelConfig> configs)
        {
            _configsById = new Dictionary<string, PanelConfig>();

            foreach (var config in configs)
            {
                if (config == null || string.IsNullOrWhiteSpace(config.Id))
                    continue;

                _configsById[config.Id] = config;
            }
        }

        public IPanelView Create(string panelId)
        {
            if (!_configsById.TryGetValue(panelId, out var config))
                throw new KeyNotFoundException($"Panel config with id '{panelId}' was not found.");

            return config switch
            {
                BattlePassPanelConfig battlePassConfig => new BattlePassPanelView(battlePassConfig),
                _ => new TestPanelView(config)
            };
        }
    }
}
