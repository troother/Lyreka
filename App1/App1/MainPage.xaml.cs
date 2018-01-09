using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPage : ContentPage
	{
        private Uri _uri;
        private HttpClient _client;

        public MainPage()
        {
            InitializeComponent();
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("X-Mashape-Key", "dA1h5JHtpfmshwPHd1KsnI9iY7YJp1IuN0Ujsn5lsJfiKrVUmv");

        }

        private async void Btn_Search(object sender, EventArgs e)
        {

            string searchArtist = E_Artist.Text;
            string searchSongTitle = E_SongTitle.Text;

            var apiResponce = await GetLyric(searchArtist, searchSongTitle);

            string Response = apiResponce.ToString();

            activityIndicator.IsRunning = false;


            L_LyricText.Text = apiResponce;

        }

        public async Task<string> GetLyric(string searchArtist, string searchSongTitle)
        {
            activityIndicator.IsRunning = true;

            _uri = new Uri($"https://musixmatchcom-musixmatch.p.mashape.com/wsr/1.1/matcher.lyrics.get?q_artist={searchArtist}&q_track={searchSongTitle}");

            var responce = await _client.GetAsync(_uri);

            var responceContent = await responce.Content.ReadAsStringAsync();

            return responceContent;
        }
    }
}
