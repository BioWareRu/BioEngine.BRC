using System;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Twitter.Exceptions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Models;

namespace BioEngine.BRC.Twitter.Service
{
    [UsedImplicitly]
    public class TwitterService
    {
        private readonly ILogger<TwitterService> _logger;

        public TwitterService(TwitterConfig config, ILogger<TwitterService> logger)
        {
            _logger = logger;
        }

        private static TwitterClient GetClient(TwitterConfig config)
        {
            return new TwitterClient(new TwitterCredentials(config.ConsumerKey,
                config.ConsumerSecret,
                config.AccessToken, config.AccessTokenSecret));
        }

        public async Task<long> CreateTweetAsync(string text, TwitterConfig config)
        {
            try
            {
                var tweet = await GetClient(config).Tweets.PublishTweetAsync(text);
                return tweet.Id;
            }
            catch (Exception exception)
            {
                if (exception is Tweetinvi.Exceptions.TwitterException twitterException)
                {
                    ProcessExceptions(twitterException);
                }

                throw;
            }
        }

        public async Task<bool> DeleteTweetAsync(long tweetId, TwitterConfig config)
        {
            try
            {
                await GetClient(config).Tweets.DestroyTweetAsync(tweetId);
                return true;
            }
            catch (Exception exception)
            {
                if (exception is Tweetinvi.Exceptions.TwitterException twitterException)
                {
                    ProcessExceptions(twitterException, "Невозможно удалить старый твит");
                }

                throw;
            }
        }

        private void ProcessExceptions(Tweetinvi.Exceptions.TwitterException exception, string? message = null)
        {
            _logger.LogError(exception.TwitterDescription);
            _logger.LogError(string.Concat(exception.TwitterExceptionInfos.SelectMany(x => x.Message)));
            if (exception.TwitterExceptionInfos.Any(x => x.Message == "Status is over 140 characters."))
            {
                throw new TooLongTweetTextException();
            }

            throw new TwitterException(!string.IsNullOrEmpty(message) ? message : exception.TwitterDescription);
        }
    }
}
