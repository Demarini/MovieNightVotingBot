using Discord.WebSocket;
using DiscordBot.MovieNightObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBot.MovieNight
{
    public class MovieNightHelper
    {
        MovieNightObject _movieNightObject;
        bool _isStarted = false;
        Stopwatch _viewMoviesWatch = new Stopwatch();
        Stopwatch _viewCommandsWatch = new Stopwatch();
        ISocketMessageChannel _channel;
        Stopwatch _displayWatch = new Stopwatch();
        public MovieNightHelper()
        {
            _movieNightObject = new MovieNightObject();
            _movieNightObject.MovieList = new List<Movies>();
            _movieNightObject.UsersVoted = new Dictionary<string, string>();
        }
        public Stopwatch DisplayWatch
        {
            get
            {
                return _displayWatch;
            }
            set
            {
                _displayWatch = value;
            }
        }
        public ISocketMessageChannel Channel
        {
            get
            {
                return _channel;
            }
            set
            {
                _channel = value;
            }
        }
        public Stopwatch ViewMoviesWatch
        {
            get
            {
                return _viewMoviesWatch;
            }
            set
            {
                _viewMoviesWatch = value;
            }
        }
        public Stopwatch ViewCommandsWatch
        {
            get
            {
                return _viewCommandsWatch;
            }
            set
            {
                _viewCommandsWatch = value;
            }
        }
        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
            set
            {
                _isStarted = value;
            }
        }
        public MovieNightObject MovieNightObject
        {
            get
            {
                return _movieNightObject;
            }
            set
            {
                _movieNightObject = value;
            }
        }
        public async Task DisplayMessage()
        {
            _displayWatch.Start();
            while (IsStarted)
            {
                if(_displayWatch.ElapsedMilliseconds > 900000)
                {
                    DisplayMovies();
                    _displayWatch.Restart();
                }
                await Task.Delay(500);
            }
        }
        public async void DisplayMovies()
        {
                string movieMessage = "**There is a movie vote currently taking place! Type !Commands to learn how to vote!**\n**Current Movies**";
                foreach (Movies m in MovieNightObject.MovieList)
                {
                    movieMessage = movieMessage + "\n-------------------------------\nMovie Name: *" + m.Name + "*\n" + "Voting ID: " + m.Id.ToString() + "\n" + "Vote Count: " + m.VoteCount.ToString();
                }
                await Channel.SendMessageAsync(movieMessage);
            }
        }
    }

