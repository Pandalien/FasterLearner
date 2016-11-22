﻿using FastLearner.Models;
using ImageSearch.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ImageSearch.Model.BingSearch;
using Acr.UserDialogs;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace FastLearner.ViewModels
{
    class CardSearchViewModel
    {
        #region Properties
        public ICommand SearchCommand { get; set; }
        public ObservableRangeCollection<ImageResult> Images { get; }
        public string Query { get; set; }
        public ImageResult SelectedItem { get; set; }
        #endregion

        public CardSearchViewModel()
        {
            Images = new ObservableRangeCollection<ImageResult>();
            SearchCommand = new Command(async () => { await Search(); });
            Query = "cat";
        }

        public async Task<bool> Search()
        {
            return await SearchForImagesAsync(Query);
        }

        public async Task<bool> SearchForImagesAsync(string query)
        {
            //Bing Image API
            var url = $"https://api.cognitive.microsoft.com/bing/v5.0/images/" + 
                      $"search?q={WebUtility.UrlEncode(query)}" +
                      $"&count=20&offset=0&mkt=en-us&safeSearch=Strict";

            var requestHeaderKey = "Ocp-Apim-Subscription-Key";
            var requestHeaderValue = CognitiveServicesKeys.BingSearch;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(requestHeaderKey, requestHeaderValue);

                    var json = await client.GetStringAsync(url);

                    var result = JsonConvert.DeserializeObject<SearchResult>(json);

                    Images.ReplaceRange(result.Images.Take(4).Select(i => new ImageResult
                    {
                        ContextLink = i.HostPageUrl,
                        FileFormat = i.EncodingFormat,
                        ImageLink = i.ContentUrl,
                        ThumbnailLink = i.ThumbnailUrl,
                        Title = i.Name,
                        Image = ImageSource.FromUri(new Uri(i.ThumbnailUrl))
                    }));
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Unable to query images: " + ex.Message);
                return false;
            }


            return true;
        }
    }
}
