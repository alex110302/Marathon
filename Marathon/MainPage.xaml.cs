using Newtonsoft.Json;
using Marathon.models;

namespace Marathon;

public partial class MainPage : ContentPage
{
    private RaceCollection raceObject;
    public MainPage()
    {
        InitializeComponent();
        Title = "Marathon Manger";
        FillPicker();
    }

    public void FillPicker()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");

        var response = client.GetAsync("races/").Result;
        var wsJson = response.Content.ReadAsStringAsync().Result;

        raceObject = JsonConvert.DeserializeObject<RaceCollection>(wsJson);

        RacePicker.ItemsSource = raceObject.races;
    }

    private void RacePicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var slectedRace = ((Picker)sender).SelectedIndex;
        var raceId = raceObject.races[slectedRace].id;
        
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");

        var response = client.GetAsync($"results/{raceId}").Result;
        var wsJson = response.Content.ReadAsStringAsync().Result;

        var resultObject = JsonConvert.DeserializeObject<ResultCollection>(wsJson);

        var cellTemplate = new DataTemplate(typeof(TextCell));
        cellTemplate.SetBinding(TextCell.TextProperty, "name");
        cellTemplate.SetBinding(TextCell.DetailProperty, "detail");

        lstResults.ItemTemplate = cellTemplate;
        lstResults.ItemsSource = resultObject.results;

        
    }
}