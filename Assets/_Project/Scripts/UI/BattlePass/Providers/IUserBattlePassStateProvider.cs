using Project.UI.BattlePass.Models;

namespace Project.UI.BattlePass.Providers
{
    public interface IUserBattlePassStateProvider
    {
        UserBattlePassState GetState();
    }
}
