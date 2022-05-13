using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galgje
{
    public class StatsPresenter
    {
        StatsRepository StatsRepo { get; set; }
        StatsView StatsView { get; set; } 

        public StatsPresenter(StatsRepository statsrepo, StatsView statsview)
        {
            StatsRepo = statsrepo;
            StatsView = statsview;  
        }

        public void GetGameStatsOver(int aantal)
        {
            GameStats stats = StatsRepo.GetGameStatsOver(aantal);
            StatsView.ShowLastGameStats(stats);
        }

        public void GetBestPlayer()
        {
            PlayerStats bestplayer = StatsRepo.GetBestPlayer();
            StatsView.ShowBestPlayer(bestplayer);
        } 

        public void GetAllPlayers()
        {
            StatsView.ShowAllPlayers(StatsRepo.GetPlayerStats());
        }
    }
}
