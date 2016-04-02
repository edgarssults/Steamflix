﻿using Ed.Steamflix.Common.Models;
using Ed.Steamflix.Common.Services;
using Ed.Steamflix.Common.ViewModels;
using Ed.Steamflix.Mocks.Repositories;
using System.Collections.Generic;

namespace Ed.Steamflix.Mocks.ViewModels
{
    public class BroadcastsPageViewModelMock : IBroadcastsPageViewModel
    {
        private readonly BroadcastService _broadcastService = new BroadcastService(new TestCommunityRepository());

        /// <summary>
        /// List of broadcasts for a game.
        /// </summary>
        public NotifyTaskCompletion<List<Broadcast>> Broadcasts
        {
            get
            {
                return new NotifyTaskCompletion<List<Broadcast>>(_broadcastService.GetBroadcastsAsync(Game.AppId));
            }
        }

        /// <summary>
        /// The game the broadcasts page is about.
        /// </summary>
        public Game Game
        {
            get
            {
                return new Game
                {
                    Name = "Witcher 3",
                    AppId = 292030
                };
            }
            set
            {
                Game = value;
            }
        }
    }
}