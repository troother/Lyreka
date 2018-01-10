using Newtonsoft.Json;
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

            string apiResponce = await GetLyric(searchArtist, searchSongTitle);

            activityIndicator.IsRunning = false;
            
            L_LyricText.Text = apiResponce;

        }

        public async Task<string> GetLyric(string searchArtist, string searchSongTitle)
        {
            activityIndicator.IsRunning = true;

            _uri = new Uri($"https://musixmatchcom-musixmatch.p.mashape.com/wsr/1.1/matcher.lyrics.get?q_artist={searchArtist}&q_track={searchSongTitle}");

            var responseFail = await _client.GetAsync(_uri);

            if (responseFail.IsSuccessStatusCode == true)
            {
                var response = await _client.GetStringAsync(_uri);
                var jsonToObject = JsonConvert.DeserializeObject<Rootobject>(response);
                L_LyricText.IsVisible = true;
                return jsonToObject.lyrics_body.Replace("\n", " ");
            }
            else
            {
                await DisplayAlert("Something went wrong!", "Unknown error occurred during your request", "Close");
                L_LyricText.IsVisible = false;
                return null;
            }
        }
    }
}

public class Rootobject
{
    public int lyrics_id { get; set; }
    public bool restricted { get; set; }
    public bool instrumental { get; set; }
    public string lyrics_body { get; set; }
    public string lyrics_language { get; set; }
    public string script_tracking_url { get; set; }
    public string pixel_tracking_url { get; set; }
    public string html_tracking_url { get; set; }
    public string lyrics_copyright { get; set; }
    public DateTime updated_time { get; set; }
}