﻿using Ed.Steamflix.Common.Repositories;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Ed.Steamflix.Mocks.Repositories
{
    public class TestCommunityRepository : ICommunityRepository
    {
        private readonly ResourceLoader _rl = ResourceLoader.GetForViewIndependentUse("Ed.Steamflix.Mocks/Resources");

        public Task<string> GetBroadcastHtmlAsync(int appId)
        {
            return Task.Run(() => _rl.GetString("BroadcastsHtml" + appId));
        }

        public Task<string> GetStatsHtmlAsync()
        {
            return Task.Run(() => _rl.GetString("StatsHtml"));
        }
    }
}